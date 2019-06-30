using System;
using System.IO;
using AppKit;
using Foundation;
using ImageIO;
using System.Globalization;  //you may need to add this DLL

//https://docs.microsoft.com/en-us/xamarin/mac/get-started/hello-mac#creating-the-interface

namespace TripPhotos
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        private double _addMinutes;

        private void MinutesToAdd(string hours, string minutes)
        {
            double temp = 0;
            _addMinutes = 0;

            if (double.TryParse(hours, out temp))
            {
                _addMinutes = temp * 60;
            }

            if (double.TryParse(minutes, out temp))
            {
                _addMinutes += temp;
            }
        }

        //http://chadkuehn.com/read-metadata-from-photos-in-c/
        private DateTime? ExtractDateTimeTaken(string path)
        {
            DateTime? dt = null;
            NSUrl url = new NSUrl(path: path, isDir: false);
            CGImageSource myImageSource = CGImageSource.FromUrl(url, null);
            var ns = new NSDictionary();
            var imageProperties = myImageSource.CopyProperties(ns, 0);

            var tiff = imageProperties.ObjectForKey(CGImageProperties.TIFFDictionary) as NSDictionary;
            var exif = imageProperties.ObjectForKey(CGImageProperties.ExifDictionary) as NSDictionary;

            //DateTaken
            var dtstr = (exif[CGImageProperties.ExifDateTimeOriginal]).ToString();
            if (string.IsNullOrEmpty(dtstr))
            {
                dtstr = (tiff[CGImageProperties.TIFFDateTime]).ToString();
            }
            if (!string.IsNullOrEmpty(dtstr))
            {
                dt = DateTime.ParseExact(dtstr, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
            }

            return dt;
        }

        private DateTime? ActualGetDateTaken(DateTime? dtaken) //Image image)
        {
            if (dtaken.HasValue)
            {

                dtaken = dtaken.Value.AddMinutes(_addMinutes);
            }

            return dtaken;
        }

        private string GetDestinationFileFullName(string fileFullName, string destinationDirectory, string destFileName, string extension)
        {
            string destinationFileFullName = string.Empty;
            destinationFileFullName = Path.ChangeExtension(Path.Combine(destinationDirectory, destFileName), extension);
            return destinationFileFullName;
        }

        //https://developer.xamarin.com/guides/mac/user-interface/dialog/#The_Open_Dialog
        private string OpenFolderDialogDialog()
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = false;
            dlg.CanChooseDirectories = true;

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    return url.Path;
                }
            }

            return string.Empty;
        }

        public void StartRenaming(string sourceDirectory, string destinationDirectory,
                                    bool renameSourceFiles, bool backup, bool prefixYear,
                                    bool processJpegs, bool processVideos)
        {
            string destFileFullName = string.Empty;
            string destFileName = string.Empty;
            string temp = string.Empty;
            string extension = string.Empty;
            string newDirectory;
            string newDestFileName;
            int totalFiles = 0;
            int tobeProcessedFiles = 0;
            int processedFiles = 0;
            //Image image = null;
            DateTime? dateTaken;
            DateTime? dtaken;
            bool processing = false;

            try
            {
                sourceDirectory = sourceDirectory.TrimEnd(Path.DirectorySeparatorChar);

                // if rename source files, then destination directory is the same as source
                if (renameSourceFiles)
                {
                    destinationDirectory = sourceDirectory;
                }

                if (!(Directory.Exists(sourceDirectory) &&
                            Directory.Exists(destinationDirectory)))
                {
                    //https://developer.xamarin.com/guides/mac/user-interface/alert/
                    using (var alert = new NSAlert())
                    {
                        alert.AlertStyle = NSAlertStyle.Critical;
                        alert.InformativeText = "Folder doesn't exist.";
                        alert.MessageText = "Error";
                        alert.BeginSheet(this.View.Window);
                    }
                }

                DirectoryInfo di = new DirectoryInfo(sourceDirectory);
                FileInfo[] files = di.GetFiles("*.*", SearchOption.AllDirectories);
                totalFiles = files.Length;

                DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.AllDirectories);

                // create backup folder in the destination folder
                if (backup)
                {
                    // if there are no sub directories, at least one BackUp has to be created
                    Directory.CreateDirectory(string.Format("{0}{1}{2}",
                                                            destinationDirectory,
                                                            Path.DirectorySeparatorChar,
                                                            "BackUp"));

                    foreach (DirectoryInfo directory in directories)
                    {
                        newDirectory = directory.FullName.Replace(sourceDirectory,
                                                                      string.Format("{0}{1}{2}",
                                                                                    destinationDirectory,
                                                                                    Path.DirectorySeparatorChar,
                                                                                    "BackUp"));
                        Directory.CreateDirectory(newDirectory);
                    }
                }

                progressBar.MinValue = 0;
                progressBar.MaxValue = totalFiles;
                progressBar.DoubleValue = processedFiles;
                progressBar.Hidden = false;

                foreach (FileInfo file in files)
                {
                    try
                    {
                        // if backup is needed, then copy file
                        // every file type
                        if (backup)
                        {
                            File.Copy(file.FullName, file.FullName.Replace(sourceDirectory,
                                                                                   string.Format("{0}{1}{2}",
                                                                                                 destinationDirectory,
                                                                                                 Path.DirectorySeparatorChar,
                                                                                                 "BackUp")));
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
                            (string.Compare(file.Extension, ".heic", true) == 0)
                           //(string.Compare(file.Extension, ".cr2", true) == 0)
                           )
                        {
                            tobeProcessedFiles++;
                            dateTaken = null;
                            dtaken = null;
                            processing = false;

                            if (((string.Compare(file.Extension, ".jpg", true) == 0) ||
                                 (string.Compare(file.Extension, ".jpeg", true) == 0))
                                    //(string.Compare(file.Extension, ".cr2", true) == 0))
                                 &&
                                 (processJpegs))
                            {
                                // images
                                try
                                {
                                    dtaken = ExtractDateTimeTaken(file.FullName);
                                    dateTaken = ActualGetDateTaken(dtaken);
                                    processing = true;
                                }
                                catch (Exception ex)
                                {
                                    richTextFailed.StringValue += string.Format("{0} - {1}\r\n", file.FullName, ex.Message);
                                }
                            }
                            else if (((string.Compare(file.Extension, ".avi", true) == 0) ||
                                      (string.Compare(file.Extension, ".mp4", true) == 0) ||
                                      (string.Compare(file.Extension, ".mov", true) == 0) ||
                                      (string.Compare(file.Extension, ".wav", true) == 0))
                                      &&
                                      (processVideos))
                            {
                                // videos
                                dtaken = file.LastWriteTime;
                                dateTaken = ActualGetDateTaken(dtaken);
                                processing = true;
                            }

                            if (processing)
                            {
                                if (dateTaken.HasValue)
                                {
                                    if (prefixYear)
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

                                if (renameSourceFiles)
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

                                // TODO: what if the random # is regenerated?
                                // then we will lose a file
                                if (File.Exists(destFileFullName))
                                {
                                    if (prefixYear)
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
                                //}

                                processedFiles++;

                                // if source files are to be deleted, then lets do it
                                if (renameSourceFiles)
                                {
                                    File.Delete(file.FullName);
                                }
                            }

                            destFileFullName = string.Empty;
                        }
                        else // not a jp*g or avi
                        {
                            if (!renameSourceFiles)
                            {
                                destFileFullName = GetDestinationFileFullName(file.FullName, destinationDirectory, destFileName, extension);
                                File.Copy(file.FullName, destFileFullName, false);
                            }
                            else
                            {
                                File.Delete(file.FullName);
                            }
                        }

                        progressBar.DoubleValue++;
                    }
                    catch (Exception ex)
                    {
                        richTextFailed.StringValue += string.Format("{0} - {1} - {2}\r\n", file.FullName, destFileFullName, ex.Message);
                    }
                }

                progressBar.Hidden = true;
                using (var alert = new NSAlert())
                {
                    alert.AlertStyle = NSAlertStyle.Informational;
                    alert.InformativeText = string.Format("{0}/{1} files processed \r\n{2} total files", processedFiles, tobeProcessedFiles, totalFiles);
                    alert.MessageText = "Done";
                    alert.BeginSheet(this.View.Window);
                }

                //if (chkOpenFolders.Checked)
                //{
                //    Process.Start(source);
                //    Process.Start(destinationDirectory);
                //}
                //}
            }
            catch (Exception ex)
            {
                using (var alert = new NSAlert())
                {
                    alert.AlertStyle = NSAlertStyle.Critical;
                    alert.InformativeText = ex.Message;
                    alert.MessageText = "Error";
                    alert.BeginSheet(this.View.Window);
                }
            }
        }

        partial void ClickedSourceFolder(Foundation.NSObject sender)
        {
            txtSourceFolder.StringValue = OpenFolderDialogDialog();
        }

        partial void ClickedDestinationFolder(Foundation.NSObject sender)
        {
            txtDestinationFolder.StringValue = OpenFolderDialogDialog();
        }

        partial void ClickedStart1(Foundation.NSObject sender)
        {
            MinutesToAdd(txtAddHours.StringValue, txtAddMinutes.StringValue);

            StartRenaming(txtSourceFolder.StringValue, txtDestinationFolder.StringValue,
                          (chkRenameSourceFiles.State == NSCellStateValue.On), (chkBackupFiles.State == NSCellStateValue.On),
                          (chkPrefixYear.State == NSCellStateValue.On), (chkProcessJpegs.State == NSCellStateValue.On),
                          (chkProcessVideos.State == NSCellStateValue.On));
        }

        partial void TextDestinationChanged(Foundation.NSObject sender)
        {
            EnableStart();
        }

        partial void TextSourceChanged(Foundation.NSObject sender)
        {
            txtDestinationFolder.StringValue = txtDestinationFolder.StringValue.TrimEnd(Path.DirectorySeparatorChar);
            EnableStart();
        }

        partial void CheckedRenameSourceFiles(Foundation.NSObject sender)
        {
            bool isNotChecked = !(chkRenameSourceFiles.State == NSCellStateValue.On);
            txtDestinationFolder.Enabled = isNotChecked;
            btnDestinationFolder.Enabled = isNotChecked;

            EnableStart();
        }

        private void EnableStart()
        {
            // there has to be source and
            // destination or rename source files
            btnStart1.Enabled = (!string.IsNullOrEmpty(txtSourceFolder.StringValue.Trim()) &&
                                  (!string.IsNullOrEmpty(txtDestinationFolder.StringValue.Trim()) || (chkRenameSourceFiles.State == NSCellStateValue.On)) &&
                                  (chkProcessVideos.State == NSCellStateValue.On) || (chkProcessJpegs.State == NSCellStateValue.On));
        }
    }
}
