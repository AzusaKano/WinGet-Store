﻿using Microsoft.Management.Deployment;
using Microsoft.Toolkit;
using System;
using Windows.UI.Xaml;

namespace WinGetStore.Controls
{
    public class PackageControlTemplateSettings : DependencyObject
    {
        #region PackageState

        /// <summary>
        /// Identifies the <see cref="PackageState"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PackageStateProperty =
            DependencyProperty.Register(
                nameof(PackageState),
                typeof(PackageState),
                typeof(PackageControlTemplateSettings),
                new PropertyMetadata(PackageState.Nominal, OnPackageStatePropertyChanged));

        /// <summary>
        /// Gets or sets the PackageState.
        /// </summary>
        public PackageState PackageState
        {
            get => (PackageState)GetValue(PackageStateProperty);
            set => SetValue(PackageStateProperty, value);
        }

        private static void OnPackageStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PackageControlTemplateSettings)d).UpdateActionButtonText();
            if (e is { OldValue: PackageState.Installing, NewValue: PackageState.Installed })
            {
                ((PackageControlTemplateSettings)d).InvokeShouldUpdatePackage();
            }
        }

        #endregion

        #region InstallerType

        /// <summary>
        /// Identifies the <see cref="InstallerType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InstallerTypeProperty =
            DependencyProperty.Register(
                nameof(InstallerType),
                typeof(PackageInstallerType),
                typeof(PackageControlTemplateSettings),
                new PropertyMetadata(PackageInstallerType.Unknown, OnInstallerTypePropertyChanged));

        /// <summary>
        /// Gets or sets the InstallerType.
        /// </summary>
        public PackageInstallerType InstallerType
        {
            get => (PackageInstallerType)GetValue(InstallerTypeProperty);
            set => SetValue(InstallerTypeProperty, value);
        }

        private static void OnInstallerTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PackageControlTemplateSettings)d).UpdateLogo();
        }

        #endregion

        #region InstallProgress

        /// <summary>
        /// Identifies the <see cref="InstallProgress"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InstallProgressProperty =
            DependencyProperty.Register(
                nameof(InstallProgress),
                typeof(InstallProgress),
                typeof(PackageControlTemplateSettings),
                new PropertyMetadata(null, OnInstallProgressPropertyChanged));

        /// <summary>
        /// Gets or sets the InstallProgress.
        /// </summary>
        public InstallProgress InstallProgress
        {
            get => (InstallProgress)GetValue(InstallProgressProperty);
            set => SetValue(InstallProgressProperty, value);
        }

        private static void OnInstallProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PackageControlTemplateSettings)d).UpdateProgress();
        }

        #endregion

        #region UninstallProgress

        /// <summary>
        /// Identifies the <see cref="UninstallProgress"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UninstallProgressProperty =
            DependencyProperty.Register(
                nameof(UninstallProgress),
                typeof(UninstallProgress),
                typeof(PackageControlTemplateSettings),
                new PropertyMetadata(null, OnUninstallProgressPropertyChanged));

        /// <summary>
        /// Gets or sets the UninstallProgress.
        /// </summary>
        public UninstallProgress UninstallProgress
        {
            get => (UninstallProgress)GetValue(UninstallProgressProperty);
            set => SetValue(UninstallProgressProperty, value);
        }

        private static void OnUninstallProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PackageControlTemplateSettings)d).UpdateProgress();
        }

        #endregion

        #region ActionButtonText

        /// <summary>
        /// Identifies the <see cref="ActionButtonText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionButtonTextProperty =
            DependencyProperty.Register(
                nameof(ActionButtonText),
                typeof(string),
                typeof(PackageControlTemplateSettings),
                null);

        /// <summary>
        /// Gets or sets the ActionButtonText.
        /// </summary>
        public string ActionButtonText
        {
            get => (string)GetValue(ActionButtonTextProperty);
            set => SetValue(ActionButtonTextProperty, value);
        }

        #endregion

        #region ProgressStatusText

        /// <summary>
        /// Identifies the <see cref="ProgressStatusText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressStatusTextProperty =
            DependencyProperty.Register(
                nameof(ProgressStatusText),
                typeof(string),
                typeof(PackageControlTemplateSettings),
                null);

        /// <summary>
        /// Gets or sets the ProgressStatusText.
        /// </summary>
        public string ProgressStatusText
        {
            get => (string)GetValue(ProgressStatusTextProperty);
            set => SetValue(ProgressStatusTextProperty, value);
        }

        #endregion

        #region ProgressValue

        /// <summary>
        /// Identifies the <see cref="ProgressValue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register(
                nameof(ProgressValue),
                typeof(double),
                typeof(PackageControlTemplateSettings),
                null);

        /// <summary>
        /// Gets or sets the ProgressValue.
        /// </summary>
        public double ProgressValue
        {
            get => (double)GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        #endregion

        #region IsIndeterminate

        /// <summary>
        /// Identifies the <see cref="IsIndeterminate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register(
                nameof(IsIndeterminate),
                typeof(bool),
                typeof(PackageControlTemplateSettings),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the IsIndeterminate.
        /// </summary>
        public bool IsIndeterminate
        {
            get => (bool)GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }

        #endregion

        #region Logo

        /// <summary>
        /// Identifies the <see cref="Logo"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LogoProperty =
            DependencyProperty.Register(
                nameof(Logo),
                typeof(Uri),
                typeof(PackageControlTemplateSettings),
                null);

        /// <summary>
        /// Gets or sets the IsIndeterminate.
        /// </summary>
        public Uri Logo
        {
            get => (Uri)GetValue(LogoProperty);
            set => SetValue(LogoProperty, value);
        }

        #endregion

        public event EventHandler<PackageState> ShouldUpdatePackage;

        private void InvokeShouldUpdatePackage() => ShouldUpdatePackage.Invoke(this, PackageState);

        private void UpdateActionButtonText()
        {
            switch (PackageState)
            {
                case PackageState.Nominal:
                    ActionButtonText = ProgressStatusText = "Install";
                    break;
                case PackageState.Installed:
                    ActionButtonText = ProgressStatusText = "Uninstall";
                    break;
                case PackageState.Installing:
                    ActionButtonText = ProgressStatusText = "Installing";
                    break;
                case PackageState.UpdateAvailable:
                    ActionButtonText = ProgressStatusText = "Upgrade";
                    break;
                case PackageState.Uninstalling:
                    ActionButtonText = ProgressStatusText = "Uninstalling";
                    break;
                default:
                    break;
            }
        }

        private void UpdateProgress()
        {
            if (PackageState == PackageState.Installing)
            {
                switch (InstallProgress.State)
                {
                    case PackageInstallProgressState.Queued:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ActionButtonText = ProgressStatusText = "Queued...";
                        break;
                    case PackageInstallProgressState.Downloading:
                        IsIndeterminate = false;
                        ProgressValue = InstallProgress.DownloadProgress * 100;
                        ProgressStatusText = InstallProgress.BytesRequired == 0
                            ? "Downloading..."
                            : $"{Converters.ToFileSizeString((long)InstallProgress.BytesDownloaded)} of {Converters.ToFileSizeString((long)InstallProgress.BytesRequired)}...";
                        ActionButtonText = $"{ProgressStatusText} • {ProgressValue:0.##}%";
                        break;
                    case PackageInstallProgressState.Installing:
                        IsIndeterminate = false;
                        ProgressValue = InstallProgress.InstallationProgress * 100;
                        ProgressStatusText = "Installing...";
                        ActionButtonText = $"{ProgressStatusText} • {ProgressValue:0.##}%";
                        break;
                    case PackageInstallProgressState.PostInstall:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ProgressStatusText = "Almost done";
                        break;
                    case PackageInstallProgressState.Finished:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ActionButtonText = ProgressStatusText = "Finished";
                        break;
                    default:
                        break;
                }
            }
            else if (PackageState == PackageState.Uninstalling)
            {
                switch (UninstallProgress.State)
                {
                    case PackageUninstallProgressState.Queued:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ActionButtonText = ProgressStatusText = "Queued...";
                        break;
                    case PackageUninstallProgressState.Uninstalling:
                        IsIndeterminate = false;
                        ProgressStatusText = "Uninstalling...";
                        ProgressValue = UninstallProgress.UninstallationProgress * 100;
                        ActionButtonText = $"{ProgressStatusText} • {ProgressValue}%";
                        break;
                    case PackageUninstallProgressState.PostUninstall:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ProgressStatusText = "Almost done";
                        break;
                    case PackageUninstallProgressState.Finished:
                        ProgressValue = 0;
                        IsIndeterminate = true;
                        ActionButtonText = ProgressStatusText = "Finished";
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateLogo() => Logo = new Uri($"ms-appx:///Assets/Logos/{InstallerType}.svg");
    }

    public enum PackageState
    {
        Nominal,
        Installed,
        Installing,
        UpdateAvailable,
        Uninstalling,
        InstallError,
        UpdateError,
        UninstallError,
    }
}
