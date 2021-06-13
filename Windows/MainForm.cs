using ImageMagick;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//https://docs.microsoft.com/en-us/xamarin/mac/get-started/hello-mac#creating-the-interface
// TODO: different thread - UI freezing
namespace Sri.TripPhotos
{
    public enum ExifOrientation : byte
    {
        Unknown = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomRight = 3,
        BottomLeft = 4,
        LeftTop = 5,
        RightTop = 6,
        RightBottom = 7,
        LeftBottom = 8,
    };

    public partial class MainForm : Form
    {
        // PropertyTagOrientation 0x0112 - we know thos prop is an integer
        private const int _orientationId = 0x0112;

        private int _totalFiles = 0;
        private int _tobeProcessedFiles = 0;
        private int _processedFiles = 0;

        private StringBuilder _message = new StringBuilder();
        private StringBuilder _specialMessage = new StringBuilder();

        private void EnableStart()
        {
            // there has to be source and
            // destination or rename source files
            btnStart.Enabled = (!string.IsNullOrEmpty(txtSource.Text.Trim()) &&
                                chkProcessVideos.Checked || chkProcessJpegs.Checked);
        }

        private Nullable<DateTime> GetDateTaken(Nullable<DateTime> dtaken) //Image image)
        {
            double minutes = 0;
            double temp = 0;
            if (dtaken.HasValue)
            {
                if (double.TryParse(txtHours.Text, out temp))
                {
                    minutes = temp * 60;
                }

                if (double.TryParse(txtMinutes.Text, out temp))
                {
                    minutes += temp;
                }

                dtaken = dtaken.Value.AddMinutes(minutes);
            }

            return dtaken;
        }

        #region OrientImage
        //http://blog.csharphelper.com/2011/08/29/use-exif-information-to-orient-an-image-properly-in-c.aspx
        //http://blog.csharphelper.com/2011/08/27/read-an-image-files-exif-orientation-data-in-c.aspx

        private ExifOrientation ImageOrientation(Image img)
        {
            // Get the index of the orientation property.    
            int orientation_index = Array.IndexOf(img.PropertyIdList, _orientationId);
            // If there is no such property, return Unknown.    
            if (orientation_index < 0)
            {
                return ExifOrientation.Unknown;
            }

            // Return the orientation value.    
            return (ExifOrientation)img.GetPropertyItem(_orientationId).Value[0];
        }

        private bool OrientImage(string sourceFileName, string destinationFileName)
        {
            Image image = null;
            bool isProperlyDone = false;

            try
            {
                image = Image.FromFile(sourceFileName);
                ExifOrientation orientation = ImageOrientation(image);

                switch (orientation)
                {
                    case ExifOrientation.TopLeft:
                        break;
                    case ExifOrientation.TopRight:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case ExifOrientation.BottomRight:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case ExifOrientation.BottomLeft:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                    case ExifOrientation.LeftTop:
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case ExifOrientation.RightTop:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case ExifOrientation.RightBottom:
                        image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        break;
                    case ExifOrientation.LeftBottom:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                // setting image orientation to top left
                //propOrient.Value[0] = 1;
                //image.SetPropertyItem(propOrient);

                image.Save(destinationFileName);
                isProperlyDone = true;
            }
            catch (Exception ex)
            {
                richTextFailed.AppendText(string.Format("{0} - {1}\r\n",
                                                        sourceFileName,
                                                        ex.Message));
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }

            return isProperlyDone;
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            this.lblVersion.Text = Application.ProductVersion;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            progressBar.Minimum = 0;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextFailed.Text = string.Empty;
            progressBar.Visible = true;

            try
            {
                DirectoryInfo di = new DirectoryInfo(txtSource.Text);
                _totalFiles = di.GetFiles("*.*", SearchOption.AllDirectories).Length;

                progressBar.Maximum = _totalFiles+1;

                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                richTextFailed.AppendText(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string destFileFullName = string.Empty;
            string destFileName = string.Empty;
            string temp = string.Empty;
            string extension = string.Empty;
            string destinationDirectory;
            string newDestFileName;

            _tobeProcessedFiles = 0;
            _processedFiles = 0;
            _message.Clear();
            _specialMessage.Clear();

            Image image = null;
            Nullable<DateTime> dateTaken;
            Nullable<DateTime> dtaken;
            bool processing = false;
           
            FileInfo[] files = null;
            List<DirectoryInfo> directories = new List<DirectoryInfo>();

            txtSource.Text = txtSource.Text.TrimEnd(Path.DirectorySeparatorChar);
            destinationDirectory = txtSource.Text;

            DirectoryInfo di = new DirectoryInfo(txtSource.Text);
            directories.Add(di);
            directories.AddRange(di.GetDirectories("*", SearchOption.AllDirectories));

            foreach (DirectoryInfo directory in directories)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    files = directory.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            extension = file.Extension;
                            destFileName = file.Name;

                            // if jp*g then process
                            // else just move to destination
                            if ((string.Compare(file.Extension, ".jpg", true) == 0) ||
                                (string.Compare(file.Extension, ".jpeg", true) == 0) ||
                                (string.Compare(file.Extension, ".avi", true) == 0) ||
                                (string.Compare(file.Extension, ".mp4", true) == 0) ||
                                (string.Compare(file.Extension, ".mov", true) == 0) ||
                                (string.Compare(file.Extension, ".wav", true) == 0) ||
                                (string.Compare(file.Extension, ".heic", true) == 0))
                            //|| (string.Compare(file.Extension, ".cr2", true) == 0)
                            {
                                _tobeProcessedFiles++;
                                dateTaken = null;
                                dtaken = null;
                                processing = false;

                                if (((string.Compare(file.Extension, ".jpg", true) == 0) ||
                                        (string.Compare(file.Extension, ".jpeg", true) == 0))
                                        &&
                                        (chkProcessJpegs.Checked))
                                // ||
                                // (string.Compare(file.Extension, ".cr2", true) == 0))
                                {
                                    // jpegs
                                    try
                                    {
                                        image = Image.FromFile(file.FullName);

                                        // http://msdn.microsoft.com/en-us/library/system.drawing.imaging.propertyitem.id.aspx
                                        //Property Item PropertyTagExifDTOrig - 0x9003 (36867) corresponds to the Date Taken  
                                        PropertyItem propDateTaken = image.GetPropertyItem(0x9003);

                                        //Convert date taken metadata to a DateTime object   
                                        dtaken = DateTime.ParseExact(Encoding.ASCII.GetString(propDateTaken.Value),
                                                                        "yyyy:MM:dd HH:mm:ss\0",
                                                                        null);

                                        dateTaken = GetDateTaken(dtaken);
                                        processing = true;
                                    }
                                    catch (Exception ex)
                                    {

                                        _message.AppendLine(string.Format("{0} - {1}", file.FullName, ex.Message));
                                    }
                                    finally
                                    {
                                        if (image != null)
                                        {
                                            image.Dispose();
                                        }
                                    }
                                }
                                else if ((string.Compare(file.Extension, ".heic", true) == 0) && chkProcessHEIC.Checked)
                                {
                                    using (MagickImage img = new MagickImage(file))
                                    {
                                        img.Format = MagickFormat.Jpeg;
                                        var exif = img.GetExifProfile();

                                        //Convert date taken metadata to a DateTime object   
                                        dtaken = DateTime.ParseExact(exif.GetValue(ExifTag.DateTimeOriginal).ToString(),
                                                                        "yyyy:MM:dd HH:mm:ss",
                                                                        null);

                                        dateTaken = GetDateTaken(dtaken);
                                        processing = true;
                                    }
                                }
                                else if (((string.Compare(file.Extension, ".avi", true) == 0) ||
                                            (string.Compare(file.Extension, ".mp4", true) == 0) ||
                                            (string.Compare(file.Extension, ".mov", true) == 0) ||
                                            (string.Compare(file.Extension, ".wav", true) == 0))
                                            &&
                                            (chkProcessVideos.Checked))
                                {
                                    // videos
                                    dtaken = file.LastWriteTime;
                                    dateTaken = GetDateTaken(dtaken);

                                    // https://markheath.net/post/how-to-get-media-file-duration-in-c
                                    try
                                    {
                                        using (var shell = ShellObject.FromParsingName(file.FullName))
                                        {
                                            dateTaken = GetDateTaken(shell.Properties.System.Media.DateEncoded.Value);
                                        }
                                    }
                                    catch { } // TODO: just eat up :)

                                    processing = true;
                                }

                                if (processing)
                                {
                                    if (dateTaken.HasValue)
                                    {
                                        if (chkPrefixYear.Checked)
                                        {
                                            destFileName = dateTaken.Value.ToString("yyyy_MMdd_HHmmss");
                                        }
                                        else
                                        {
                                            destFileName = dateTaken.Value.ToString("MMdd_HHmmss");
                                        }
                                    }
                                    else
                                    {
                                        destFileName = Path.GetFileNameWithoutExtension(file.Name);
                                    }

                                    destFileFullName = GetDestinationFileFullName(file.FullName,
                                                                                    Directory.GetParent(file.FullName).FullName,
                                                                                    destFileName,
                                                                                    extension);

                                    // already renamed / don't need to process
                                    if (string.Compare(file.FullName, destFileFullName, true) != 0)
                                    {
                                        // TODO: what if the same random # is regenerated?
                                        // then we will lose a file
                                        if (File.Exists(destFileFullName))
                                        {
                                            if (chkPrefixYear.Checked)
                                            {
                                                newDestFileName = string.Format("{0}_{1}",
                                                                            dateTaken.Value.ToString("yyyy_MMdd_HHmmss"),
                                                                            new Random().Next(100));
                                            }
                                            else
                                            {
                                                newDestFileName = string.Format("{0}_{1}",
                                                                            dateTaken.Value.ToString("MMdd_HHmmss"),
                                                                            new Random().Next(100));
                                            }

                                            destFileFullName = destFileFullName.Replace(destFileName, newDestFileName);
                                        }

                                        //if (chkAdjustOrientation.Checked && !OrientImage (file.FullName, destFileFullName))
                                        //{
                                        File.Copy(file.FullName, destFileFullName, false);
                                        File.Delete(file.FullName);
                                    }

                                    if ((string.Compare(file.Extension, ".heic", true) == 0) && chkConvertHEIC.Checked)
                                    {
                                        using (MagickImage img = new MagickImage(destFileFullName))
                                        {
                                            img.Format = MagickFormat.Jpeg;
                                            img.Write(Regex.Replace(destFileFullName, ".heic", ".jpg", RegexOptions.IgnoreCase));
                                            File.Delete(destFileFullName);
                                        }
                                    }

                                    _processedFiles++;
                                }

                                destFileFullName = string.Empty;
                            }

                            worker.ReportProgress(_tobeProcessedFiles);
                        }
                        catch (Exception ex)
                        {
                            _specialMessage.AppendLine(string.Format("{0} - {1} - {2}", file.FullName, destFileFullName, ex.Message));
                            worker.ReportProgress(-100);
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -100)
            {
                richTextFailed.AppendText(_specialMessage.ToString());
                _specialMessage.Clear();
            }
            else
            {
                progressBar.Value++;
                label2.Text = string.Format("{0}/{1}", e.ProgressPercentage, _totalFiles);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            richTextFailed.AppendText(_message.ToString());

            progressBar.Visible = false;
            if (e.Cancelled == true)
            {
                richTextFailed.AppendText("Canceled!");
            }
            else if (e.Error != null)
            {
                richTextFailed.AppendText("Error: " + e.Error.Message);
            }
            else
            {
                richTextFailed.AppendText(string.Format("{0}/{1} files processed \r\n{2} total files",
                                              _processedFiles, _tobeProcessedFiles, _totalFiles));
            }
        }

        private string GetDestinationFileFullName(string fileFullName, string destinationDirectory, string destFileName, string extension)
        {
            string destinationFileFullName = string.Empty;
            destinationFileFullName = Path.ChangeExtension(Path.Combine(destinationDirectory,
                                                                        destFileName),
                                                           extension);
            return destinationFileFullName;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            folderBrowserSource.SelectedPath = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName,
                                                            "SkyDrive");

            if (folderBrowserSource.ShowDialog(this) == DialogResult.OK)
            {
                txtSource.Text = folderBrowserSource.SelectedPath;
            }
        }

        private void txtSource_Changed(object sender, EventArgs e)
        {
            EnableStart();
        }

        private void chkProcess_CheckedChanged(object sender, EventArgs e)
        {
            EnableStart();
        }

        private void ChkProcessHEIC_CheckedChanged(object sender, EventArgs e)
        {
            chkConvertHEIC.Enabled = chkProcessHEIC.Checked;
            chkConvertHEIC.Checked = chkProcessHEIC.Checked;
        }

    }
}
