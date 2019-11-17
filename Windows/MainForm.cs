using ImageMagick;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

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

        private void EnableStart()
        {
            // there has to be source and
            // destination or rename source files
            btnStart.Enabled = (!string.IsNullOrEmpty(txtSource.Text.Trim()) &&
                                (!string.IsNullOrEmpty(txtDestination.Text.Trim()) || chkRenameSourceFiles.Checked) &&
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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string destFileFullName = string.Empty;
            string destFileName = string.Empty;
            string temp = string.Empty;
            string extension = string.Empty;
            string newDirectory;
            string destinationDirectory;
            string newDestFileName;
            int totalFiles = 0;
            int tobeProcessedFiles = 0;
            int processedFiles = 0;
            Image image = null;
            Nullable<DateTime> dateTaken;
            Nullable<DateTime> dtaken;
            bool processing = false;
            richTextFailed.Text = string.Empty;

            try
            {
                txtSource.Text = txtSource.Text.TrimEnd(Path.DirectorySeparatorChar);

                // if rename source files, then destination directory is the same as source
                if (chkRenameSourceFiles.Checked)
                {
                    destinationDirectory = txtSource.Text;
                }
                else
                {
                    destinationDirectory = txtDestination.Text;
                }

                if (!(Directory.Exists(txtSource.Text) &&
                        Directory.Exists(destinationDirectory)))
                {
                    MessageBox.Show(this,
                                    "Folder doesn't exist",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    return;
                }

                DirectoryInfo di = new DirectoryInfo(txtSource.Text);
                FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
                totalFiles = files.Length;

                DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.AllDirectories);
                string backupFolder = "backup_" + new Random().Next(100).ToString();

                // create backup folder in the destination folder
                if (chkBackup.Checked)
                {
                    // if there are no sub directories, at least one BackUp has to be created
                    Directory.CreateDirectory(string.Format("{0}{1}{2}",
                                                            destinationDirectory,
                                                            Path.DirectorySeparatorChar,
                                                            backupFolder));

                    foreach (DirectoryInfo directory in directories)
                    {
                        newDirectory = directory.FullName.Replace(txtSource.Text,
                                                                  string.Format("{0}{1}{2}",
                                                                                destinationDirectory,
                                                                                Path.DirectorySeparatorChar,
                                                                                backupFolder));
                        Directory.CreateDirectory(newDirectory);
                    }
                }

                progressBar.Minimum = 0;
                progressBar.Maximum = totalFiles;
                progressBar.Value = processedFiles;
                progressBar.Visible = true;

                foreach (FileInfo file in files)
                {
                    try
                    {
                        // if backup is needed, then copy file
                        // every file type
                        if (chkBackup.Checked)
                        {
                            File.Copy(file.FullName, file.FullName.Replace(txtSource.Text,
                                                                           string.Format("{0}{1}{2}",
                                                                                         destinationDirectory,
                                                                                         Path.DirectorySeparatorChar,
                                                                                         backupFolder)));
                        }

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
                            tobeProcessedFiles++;
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
                                    richTextFailed.AppendText(string.Format("{0} - {1}\r\n",
                                                                            file.FullName,
                                                                            ex.Message));
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
                                    ExifProfile exif = img.GetExifProfile();

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

                                if (chkRenameSourceFiles.Checked)
                                {
                                    destFileFullName = GetDestinationFileFullName(file.FullName,
                                                                                    Directory.GetParent(file.FullName).FullName,
                                                                                    destFileName,
                                                                                    extension);
                                }
                                else
                                {
                                    destFileFullName = GetDestinationFileFullName(file.FullName,
                                                                                    destinationDirectory,
                                                                                    destFileName,
                                                                                    extension);
                                }

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
                                if ((string.Compare(file.Extension, ".heic", true) == 0) && chkConvertHEIC.Checked)
                                {
                                    using (MagickImage img = new MagickImage(destFileFullName))
                                    {
                                        img.Format = MagickFormat.Jpeg;
                                        img.Write(Regex.Replace(destFileFullName, ".heic", ".jpg", RegexOptions.IgnoreCase));
                                        File.Delete(destFileFullName);
                                    }
                                }
                                //}

                                processedFiles++;

                                // if renaming source files then lets delete
                                if (chkRenameSourceFiles.Checked)
                                {
                                    File.Delete(file.FullName);
                                }
                            }

                            destFileFullName = string.Empty;
                        }
                        else // not a jp*g or avi
                        {
                            if (!chkRenameSourceFiles.Checked)
                            {
                                destFileFullName = GetDestinationFileFullName(file.FullName, destinationDirectory, destFileName, extension);
                                File.Copy(file.FullName, destFileFullName, false);

                                // if renaming source files then lets delete
                                if (chkRenameSourceFiles.Checked)
                                {
                                    File.Delete(file.FullName);
                                }
                            }
                        }

                        progressBar.Value++;
                    }
                    catch (Exception ex)
                    {
                        richTextFailed.AppendText(string.Format("{0} - {1} - {2}\r\n",
                                                                file.FullName,
                                                                destFileFullName,
                                                                ex.Message));
                    }
                }

                // Delete empty source directories
                // if (chkDeleteSourceFiles.Checked)
                // {
                for (int i = directories.Length - 1; i >= 0; i--)
                {
                    if (directories[i].GetFiles().Length == 0)
                    {
                        directories[i].Delete();
                    }
                }
                // }

                progressBar.Visible = false;

                MessageBox.Show(this,
                                string.Format("{0}/{1} files processed \r\n{2} total files",
                                              processedFiles, tobeProcessedFiles, totalFiles),
                                "File renamer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                if (chkOpenFolders.Checked)
                {
                    Process.Start(txtSource.Text);
                    Process.Start(destinationDirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void btnDestination_Click(object sender, EventArgs e)
        {
            folderBrowserDestination.SelectedPath = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName,
                                                                 "SkyDrive");

            if (folderBrowserDestination.ShowDialog(this) == DialogResult.OK)
            {
                txtDestination.Text = folderBrowserDestination.SelectedPath;
            }
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

        private void txtDestination_Changed(object sender, EventArgs e)
        {
            EnableStart();
        }

        private void txtSource_Changed(object sender, EventArgs e)
        {
            txtDestination.Text = txtDestination.Text.TrimEnd(Path.DirectorySeparatorChar);
            EnableStart();
        }

        private void chkRenameSourceFiles_CheckedChanged(object sender, EventArgs e)
        {
            bool isNotChecked = !chkRenameSourceFiles.Checked;

            txtDestination.Enabled = isNotChecked;
            btnDestination.Enabled = isNotChecked;

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
