using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Comercial_Estefannny.Services
{
    public class ThemeResourceUpdater
    {
        private static ThemeResourceUpdater _instance;
        public static ThemeResourceUpdater Instance => _instance ??= new ThemeResourceUpdater();

        private readonly Dictionary<string, string> _brushResourceKeys = new()
        {
            // Color to Brush mapping
            ["PrimaryColor"] = "PrimaryBrush",
            ["PrimaryDarkColor"] = "PrimaryDarkBrush",
            ["SecondaryColor"] = "SecondaryBrush",
            ["AccentColor"] = "AccentBrush",
            ["BackgroundColor"] = "BackgroundBrush",
            ["SurfaceColor"] = "SurfaceBrush",
            ["CardBackgroundColor"] = "CardBackgroundBrush",
            ["TextPrimaryColor"] = "TextPrimaryBrush",
            ["TextSecondaryColor"] = "TextSecondaryBrush",
            ["TextOnPrimaryColor"] = "TextOnPrimaryBrush",
            ["BorderColor"] = "BorderBrush",
            ["ShadowColor"] = "ShadowBrush",
            ["SuccessColor"] = "SuccessBrush",
            ["WarningColor"] = "WarningBrush",
            ["ErrorColor"] = "ErrorBrush",
            ["InfoColor"] = "InfoBrush",
        };

        private ThemeResourceUpdater()
        {
            // Subscribe to theme changes
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(object sender, ThemeType newTheme)
        {
            UpdateApplicationResources(newTheme);
        }

        public void UpdateApplicationResources(ThemeType theme)
        {
            try
            {
                var appResources = Application.Current?.Resources;
                if (appResources == null) return;

                // Get theme colors from ThemeManager
                var themeManager = ThemeManager.Instance;
                var reflection = typeof(ThemeManager).GetField("_themes", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (reflection?.GetValue(themeManager) is Dictionary<ThemeType, Dictionary<string, object>> themes &&
                    themes.TryGetValue(theme, out var themeColors))
                {
                    // Update color resources
                    foreach (var colorEntry in themeColors)
                    {
                        if (colorEntry.Value is Color color)
                        {
                            // Update the color resource
                            appResources[colorEntry.Key] = color;
                            
                            // Update corresponding brush resource
                            if (_brushResourceKeys.TryGetValue(colorEntry.Key, out var brushKey))
                            {
                                appResources[brushKey] = new SolidColorBrush(color);
                            }
                        }
                    }

                    // Update gradient resources
                    UpdateGradientResources(appResources, themeColors);

                    // Update hover and focus resources
                    UpdateInteractionResources(appResources, themeColors);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to update theme resources: {ex.Message}");
            }
        }

        private void UpdateGradientResources(ResourceDictionary resources, Dictionary<string, object> themeColors)
        {
            try
            {
                // Primary Gradient
                if (themeColors.TryGetValue("PrimaryColor", out var primaryObj) && primaryObj is Color primary &&
                    themeColors.TryGetValue("PrimaryDarkColor", out var primaryDarkObj) && primaryDarkObj is Color primaryDark)
                {
                    var primaryGradient = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1)
                    };
                    primaryGradient.GradientStops.Add(new GradientStop(primary, 0));
                    primaryGradient.GradientStops.Add(new GradientStop(primaryDark, 1));
                    resources["PrimaryGradient"] = primaryGradient;
                }

                // Secondary Gradient
                if (themeColors.TryGetValue("SecondaryColor", out var secondaryObj) && secondaryObj is Color secondary)
                {
                    var secondaryDark = Color.FromRgb(
                        (byte)(secondary.R * 0.8),
                        (byte)(secondary.G * 0.8),
                        (byte)(secondary.B * 0.8)
                    );
                    
                    var secondaryGradient = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1)
                    };
                    secondaryGradient.GradientStops.Add(new GradientStop(secondary, 0));
                    secondaryGradient.GradientStops.Add(new GradientStop(secondaryDark, 1));
                    resources["SecondaryGradient"] = secondaryGradient;
                }

                // Background Gradient
                if (themeColors.TryGetValue("BackgroundColor", out var bgObj) && bgObj is Color background &&
                    themeColors.TryGetValue("SurfaceColor", out var surfaceObj) && surfaceObj is Color surface)
                {
                    var backgroundGradient = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1)
                    };
                    backgroundGradient.GradientStops.Add(new GradientStop(background, 0));
                    backgroundGradient.GradientStops.Add(new GradientStop(surface, 1));
                    resources["BackgroundGradient"] = backgroundGradient;
                }

                // Status Gradients
                UpdateStatusGradients(resources, themeColors);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to update gradient resources: {ex.Message}");
            }
        }

        private void UpdateStatusGradients(ResourceDictionary resources, Dictionary<string, object> themeColors)
        {
            var statusColors = new[] { "SuccessColor", "WarningColor", "ErrorColor", "InfoColor" };
            var gradientNames = new[] { "SuccessGradient", "WarningGradient", "ErrorGradient", "InfoGradient" };

            for (int i = 0; i < statusColors.Length; i++)
            {
                if (themeColors.TryGetValue(statusColors[i], out var colorObj) && colorObj is Color color)
                {
                    var darkerColor = Color.FromRgb(
                        (byte)(color.R * 0.8),
                        (byte)(color.G * 0.8),
                        (byte)(color.B * 0.8)
                    );

                    var gradient = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1)
                    };
                    gradient.GradientStops.Add(new GradientStop(color, 0));
                    gradient.GradientStops.Add(new GradientStop(darkerColor, 1));
                    resources[gradientNames[i]] = gradient;
                }
            }
        }

        private void UpdateInteractionResources(ResourceDictionary resources, Dictionary<string, object> themeColors)
        {
            try
            {
                // Hover colors (slightly darker)
                if (themeColors.TryGetValue("PrimaryColor", out var primaryObj) && primaryObj is Color primary)
                {
                    var hoverColor = Color.FromRgb(
                        (byte)(primary.R * 0.9),
                        (byte)(primary.G * 0.9),
                        (byte)(primary.B * 0.9)
                    );
                    resources["PrimaryHoverBrush"] = new SolidColorBrush(hoverColor);
                }

                if (themeColors.TryGetValue("SecondaryColor", out var secondaryObj) && secondaryObj is Color secondary)
                {
                    var hoverColor = Color.FromRgb(
                        (byte)(secondary.R * 0.9),
                        (byte)(secondary.G * 0.9),
                        (byte)(secondary.B * 0.9)
                    );
                    resources["SecondaryHoverBrush"] = new SolidColorBrush(hoverColor);
                }

                // Focus colors
                if (themeColors.TryGetValue("PrimaryColor", out var focusObj) && focusObj is Color focus)
                {
                    resources["FocusBrush"] = new SolidColorBrush(focus);
                    
                    var focusOutline = Color.FromArgb(128, focus.R, focus.G, focus.B); // 50% opacity
                    resources["FocusOutlineBrush"] = new SolidColorBrush(focusOutline);
                }

                // Disabled colors
                if (themeColors.TryGetValue("TextSecondaryColor", out var disabledObj) && disabledObj is Color disabled)
                {
                    resources["DisabledBrush"] = new SolidColorBrush(disabled);
                    resources["DisabledTextBrush"] = new SolidColorBrush(disabled);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to update interaction resources: {ex.Message}");
            }
        }

        public void ForceResourceUpdate()
        {
            UpdateApplicationResources(ThemeManager.Instance.CurrentTheme);
        }
    }
}
