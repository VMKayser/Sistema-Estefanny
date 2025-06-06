using System;
using System.Windows;
using System.Windows.Controls;

namespace Comercial_Estefannny.Services
{
    public enum ScreenSize
    {
        Small,      // < 1024px width
        Medium,     // 1024px - 1440px width
        Large,      // 1440px - 1920px width
        ExtraLarge  // > 1920px width
    }

    public class ResponsiveLayoutManager
    {
        private static ResponsiveLayoutManager _instance;
        public static ResponsiveLayoutManager Instance => _instance ??= new ResponsiveLayoutManager();

        public event EventHandler<ScreenSize> ScreenSizeChanged;

        private ScreenSize _currentScreenSize;
        public ScreenSize CurrentScreenSize
        {
            get => _currentScreenSize;
            private set
            {
                if (_currentScreenSize != value)
                {
                    _currentScreenSize = value;
                    ScreenSizeChanged?.Invoke(this, value);
                }
            }
        }

        private ResponsiveLayoutManager()
        {
            // Initialize with current screen size
            UpdateScreenSize(SystemParameters.PrimaryScreenWidth);
        }

        public void UpdateScreenSize(double screenWidth)
        {
            CurrentScreenSize = screenWidth switch
            {
                < 1024 => ScreenSize.Small,
                >= 1024 and < 1440 => ScreenSize.Medium,
                >= 1440 and < 1920 => ScreenSize.Large,
                _ => ScreenSize.ExtraLarge
            };
        }

        public GridLength GetSidebarWidth()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => new GridLength(60),      // Collapsed sidebar
                ScreenSize.Medium => new GridLength(200),    // Narrow sidebar
                ScreenSize.Large => new GridLength(280),     // Standard sidebar
                ScreenSize.ExtraLarge => new GridLength(320), // Wide sidebar
                _ => new GridLength(280)
            };
        }

        public double GetCardMinWidth()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => 280,
                ScreenSize.Medium => 320,
                ScreenSize.Large => 360,
                ScreenSize.ExtraLarge => 400,
                _ => 360
            };
        }

        public int GetDataGridColumns()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => 3,      // Show only essential columns
                ScreenSize.Medium => 5,     // Show most columns
                ScreenSize.Large => 7,      // Show all columns
                ScreenSize.ExtraLarge => 8, // Show all columns with extra details
                _ => 7
            };
        }

        public double GetFontSizeMultiplier()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => 0.9,
                ScreenSize.Medium => 1.0,
                ScreenSize.Large => 1.0,
                ScreenSize.ExtraLarge => 1.1,
                _ => 1.0
            };
        }

        public Thickness GetContentMargin()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => new Thickness(10),
                ScreenSize.Medium => new Thickness(15),
                ScreenSize.Large => new Thickness(20),
                ScreenSize.ExtraLarge => new Thickness(25),
                _ => new Thickness(20)
            };
        }

        public double GetButtonHeight()
        {
            return CurrentScreenSize switch
            {
                ScreenSize.Small => 35,
                ScreenSize.Medium => 40,
                ScreenSize.Large => 45,
                ScreenSize.ExtraLarge => 50,
                _ => 45
            };
        }

        public void ApplyResponsiveLayout(FrameworkElement element)
        {
            if (element == null) return;

            try
            {
                // Apply responsive margins
                if (element is Panel || element is Border)
                {
                    element.Margin = GetContentMargin();
                }

                // Apply responsive font sizes to text elements
                if (element is TextBlock textBlock)
                {
                    var currentFontSize = textBlock.FontSize;
                    if (!double.IsNaN(currentFontSize))
                    {
                        textBlock.FontSize = currentFontSize * GetFontSizeMultiplier();
                    }
                }

                // Apply responsive button sizes
                if (element is Button button)
                {
                    if (double.IsNaN(button.Height))
                    {
                        button.Height = GetButtonHeight();
                    }
                }

                // Recursively apply to child elements
                if (element is Panel panel)
                {
                    foreach (UIElement child in panel.Children)
                    {
                        if (child is FrameworkElement childElement)
                        {
                            ApplyResponsiveLayout(childElement);
                        }
                    }
                }
                else if (element is ContentControl contentControl && contentControl.Content is FrameworkElement content)
                {
                    ApplyResponsiveLayout(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying responsive layout: {ex.Message}");
            }
        }

        public void RegisterWindowForResponsiveUpdates(Window window)
        {
            if (window == null) return;

            window.SizeChanged += (sender, args) =>
            {
                UpdateScreenSize(args.NewSize.Width);
            };

            // Apply initial responsive layout
            ApplyResponsiveLayout(window);
        }
    }
}
