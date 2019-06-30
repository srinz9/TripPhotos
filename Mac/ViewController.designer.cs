// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TripPhotos
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton btnDestinationFolder { get; set; }

		[Outlet]
		AppKit.NSButton btnStart1 { get; set; }

		[Outlet]
		AppKit.NSButton chkBackupFiles { get; set; }

		[Outlet]
		AppKit.NSButton chkPrefixYear { get; set; }

		[Outlet]
		AppKit.NSButton chkProcessJpegs { get; set; }

		[Outlet]
		AppKit.NSButton chkProcessVideos { get; set; }

		[Outlet]
		AppKit.NSButton chkRenameSourceFiles { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator progressBar { get; set; }

		[Outlet]
		AppKit.NSTextField richTextFailed { get; set; }

		[Outlet]
		AppKit.NSTextField txtAddHours { get; set; }

		[Outlet]
		AppKit.NSTextField txtAddMinutes { get; set; }

		[Outlet]
		AppKit.NSTextField txtDestinationFolder { get; set; }

		[Outlet]
		AppKit.NSTextField txtSourceFolder { get; set; }

		[Action ("CheckedRenameSourceFiles:")]
		partial void CheckedRenameSourceFiles (Foundation.NSObject sender);

		[Action ("ClickedDestinationFolder:")]
		partial void ClickedDestinationFolder (Foundation.NSObject sender);

		[Action ("ClickedProcess:")]
		partial void ClickedProcess (Foundation.NSObject sender);

		[Action ("ClickedSourceFolder:")]
		partial void ClickedSourceFolder (Foundation.NSObject sender);

		[Action ("ClickedStart:")]
		partial void ClickedStart (Foundation.NSObject sender);

		[Action ("ClickedStart1:")]
		partial void ClickedStart1 (Foundation.NSObject sender);

		[Action ("TextDestinationChanged:")]
		partial void TextDestinationChanged (Foundation.NSObject sender);

		[Action ("TextSourceChanged:")]
		partial void TextSourceChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDestinationFolder != null) {
				btnDestinationFolder.Dispose ();
				btnDestinationFolder = null;
			}

			if (btnStart1 != null) {
				btnStart1.Dispose ();
				btnStart1 = null;
			}

			if (chkBackupFiles != null) {
				chkBackupFiles.Dispose ();
				chkBackupFiles = null;
			}

			if (chkPrefixYear != null) {
				chkPrefixYear.Dispose ();
				chkPrefixYear = null;
			}

			if (chkProcessJpegs != null) {
				chkProcessJpegs.Dispose ();
				chkProcessJpegs = null;
			}

			if (chkProcessVideos != null) {
				chkProcessVideos.Dispose ();
				chkProcessVideos = null;
			}

			if (chkRenameSourceFiles != null) {
				chkRenameSourceFiles.Dispose ();
				chkRenameSourceFiles = null;
			}

			if (progressBar != null) {
				progressBar.Dispose ();
				progressBar = null;
			}

			if (richTextFailed != null) {
				richTextFailed.Dispose ();
				richTextFailed = null;
			}

			if (txtAddHours != null) {
				txtAddHours.Dispose ();
				txtAddHours = null;
			}

			if (txtAddMinutes != null) {
				txtAddMinutes.Dispose ();
				txtAddMinutes = null;
			}

			if (txtDestinationFolder != null) {
				txtDestinationFolder.Dispose ();
				txtDestinationFolder = null;
			}

			if (txtSourceFolder != null) {
				txtSourceFolder.Dispose ();
				txtSourceFolder = null;
			}
		}
	}
}
