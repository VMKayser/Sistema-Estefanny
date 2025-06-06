using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Comercial_Estefannny.Services
{
    public enum ThemeType
    {
        Light,
        Dark,
        Blue,
        Green,
        Purple
    }

    public class ThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();

        public event EventHandler<ThemeType> ThemeChanged;

        private ThemeType _currentTheme = ThemeType.Light;
        public ThemeType CurrentTheme 
        { 
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ThemeChanged?.Invoke(this, value);
                }
            }
        }

        private readonly Dictionary<ThemeType, Dictionary<string, object>> _themes;

        private ThemeManager()
        {
            _themes = InitializeThemes();
        }        public void SetTheme(ThemeType theme)
        {
            CurrentTheme = theme;
            ApplyTheme(theme);
            
            // Update UI resources
            ThemeResourceUpdater.Instance.UpdateApplicationResources(theme);
        }

        private void ApplyTheme(ThemeType theme)
        {
            if (!_themes.ContainsKey(theme)) return;

            var themeResources = _themes[theme];
            var appResources = Application.Current.Resources;

            // Update application resources with theme colors
            foreach (var resource in themeResources)
            {
                appResources[resource.Key] = resource.Value;
            }
        }

        private Dictionary<ThemeType, Dictionary<string, object>> InitializeThemes()
        {
            return new Dictionary<ThemeType, Dictionary<string, object>>
            {
                [ThemeType.Light] = new Dictionary<string, object>
                {
                    // Primary Colors
                    ["PrimaryColor"] = Color.FromRgb(52, 152, 219),      // #3498DB
                    ["PrimaryDarkColor"] = Color.FromRgb(41, 128, 185),  // #2980B9
                    ["SecondaryColor"] = Color.FromRgb(155, 89, 182),     // #9B59B6
                    ["AccentColor"] = Color.FromRgb(230, 126, 34),        // #E67E22
                    
                    // Background Colors
                    ["BackgroundColor"] = Color.FromRgb(248, 249, 250),   // #F8F9FA
                    ["SurfaceColor"] = Colors.White,                       // #FFFFFF
                    ["CardBackgroundColor"] = Colors.White,               // #FFFFFF
                    
                    // Text Colors
                    ["TextPrimaryColor"] = Color.FromRgb(33, 37, 41),     // #212529
                    ["TextSecondaryColor"] = Color.FromRgb(108, 117, 125), // #6C757D
                    ["TextOnPrimaryColor"] = Colors.White,                 // #FFFFFF
                    
                    // Border and Shadow Colors
                    ["BorderColor"] = Color.FromRgb(222, 226, 230),       // #DEE2E6
                    ["ShadowColor"] = Color.FromArgb(25, 0, 0, 0),        // 10% Black
                    
                    // Status Colors
                    ["SuccessColor"] = Color.FromRgb(40, 167, 69),        // #28A745
                    ["WarningColor"] = Color.FromRgb(255, 193, 7),        // #FFC107
                    ["ErrorColor"] = Color.FromRgb(220, 53, 69),          // #DC3545
                    ["InfoColor"] = Color.FromRgb(23, 162, 184),          // #17A2B8
                },

                [ThemeType.Dark] = new Dictionary<string, object>
                {
                    // Primary Colors
                    ["PrimaryColor"] = Color.FromRgb(100, 181, 246),      // #64B5F6
                    ["PrimaryDarkColor"] = Color.FromRgb(66, 165, 245),   // #42A5F5
                    ["SecondaryColor"] = Color.FromRgb(186, 104, 200),     // #BA68C8
                    ["AccentColor"] = Color.FromRgb(255, 167, 38),         // #FFA726
                    
                    // Background Colors
                    ["BackgroundColor"] = Color.FromRgb(18, 18, 18),      // #121212
                    ["SurfaceColor"] = Color.FromRgb(30, 30, 30),         // #1E1E1E
                    ["CardBackgroundColor"] = Color.FromRgb(40, 40, 40),  // #282828
                    
                    // Text Colors
                    ["TextPrimaryColor"] = Color.FromRgb(255, 255, 255),  // #FFFFFF
                    ["TextSecondaryColor"] = Color.FromRgb(158, 158, 158), // #9E9E9E
                    ["TextOnPrimaryColor"] = Color.FromRgb(18, 18, 18),   // #121212
                    
                    // Border and Shadow Colors
                    ["BorderColor"] = Color.FromRgb(66, 66, 66),          // #424242
                    ["ShadowColor"] = Color.FromArgb(50, 0, 0, 0),        // 20% Black
                    
                    // Status Colors
                    ["SuccessColor"] = Color.FromRgb(76, 175, 80),        // #4CAF50
                    ["WarningColor"] = Color.FromRgb(255, 235, 59),       // #FFEB3B
                    ["ErrorColor"] = Color.FromRgb(244, 67, 54),          // #F44336
                    ["InfoColor"] = Color.FromRgb(33, 150, 243),          // #2196F3
                },

                [ThemeType.Blue] = new Dictionary<string, object>
                {
                    // Primary Colors
                    ["PrimaryColor"] = Color.FromRgb(25, 118, 210),       // #1976D2
                    ["PrimaryDarkColor"] = Color.FromRgb(21, 101, 192),   // #1565C0
                    ["SecondaryColor"] = Color.FromRgb(0, 172, 193),       // #00ACC1
                    ["AccentColor"] = Color.FromRgb(255, 111, 97),         // #FF6F61
                    
                    // Background Colors
                    ["BackgroundColor"] = Color.FromRgb(240, 248, 255),   // #F0F8FF
                    ["SurfaceColor"] = Color.FromRgb(250, 252, 255),      // #FAFCFF
                    ["CardBackgroundColor"] = Colors.White,               // #FFFFFF
                    
                    // Text Colors
                    ["TextPrimaryColor"] = Color.FromRgb(13, 60, 97),     // #0D3C61
                    ["TextSecondaryColor"] = Color.FromRgb(69, 90, 100),   // #455A64
                    ["TextOnPrimaryColor"] = Colors.White,                 // #FFFFFF
                    
                    // Border and Shadow Colors
                    ["BorderColor"] = Color.FromRgb(187, 222, 251),       // #BBDEFB
                    ["ShadowColor"] = Color.FromArgb(30, 25, 118, 210),   // Primary with alpha
                    
                    // Status Colors
                    ["SuccessColor"] = Color.FromRgb(46, 125, 50),        // #2E7D32
                    ["WarningColor"] = Color.FromRgb(245, 124, 0),        // #F57C00
                    ["ErrorColor"] = Color.FromRgb(211, 47, 47),          // #D32F2F
                    ["InfoColor"] = Color.FromRgb(2, 136, 209),           // #0288D1
                },

                [ThemeType.Green] = new Dictionary<string, object>
                {
                    // Primary Colors
                    ["PrimaryColor"] = Color.FromRgb(56, 142, 60),        // #388E3C
                    ["PrimaryDarkColor"] = Color.FromRgb(46, 125, 50),    // #2E7D32
                    ["SecondaryColor"] = Color.FromRgb(102, 187, 106),     // #66BB6A
                    ["AccentColor"] = Color.FromRgb(255, 193, 7),          // #FFC107
                    
                    // Background Colors
                    ["BackgroundColor"] = Color.FromRgb(245, 254, 245),   // #F5FEF5
                    ["SurfaceColor"] = Color.FromRgb(250, 255, 250),      // #FAFFFA
                    ["CardBackgroundColor"] = Colors.White,               // #FFFFFF
                    
                    // Text Colors
                    ["TextPrimaryColor"] = Color.FromRgb(27, 94, 32),     // #1B5E20
                    ["TextSecondaryColor"] = Color.FromRgb(76, 175, 80),   // #4CAF50
                    ["TextOnPrimaryColor"] = Colors.White,                 // #FFFFFF
                    
                    // Border and Shadow Colors
                    ["BorderColor"] = Color.FromRgb(200, 230, 201),       // #C8E6C9
                    ["ShadowColor"] = Color.FromArgb(30, 56, 142, 60),    // Primary with alpha
                    
                    // Status Colors
                    ["SuccessColor"] = Color.FromRgb(67, 160, 71),        // #43A047
                    ["WarningColor"] = Color.FromRgb(251, 140, 0),        // #FB8C00
                    ["ErrorColor"] = Color.FromRgb(229, 57, 53),          // #E53935
                    ["InfoColor"] = Color.FromRgb(41, 182, 246),          // #29B6F6
                },

                [ThemeType.Purple] = new Dictionary<string, object>
                {
                    // Primary Colors
                    ["PrimaryColor"] = Color.FromRgb(123, 31, 162),       // #7B1FA2
                    ["PrimaryDarkColor"] = Color.FromRgb(106, 27, 154),   // #6A1B9A
                    ["SecondaryColor"] = Color.FromRgb(171, 71, 188),      // #AB47BC
                    ["AccentColor"] = Color.FromRgb(255, 61, 0),           // #FF3D00
                    
                    // Background Colors
                    ["BackgroundColor"] = Color.FromRgb(252, 245, 255),   // #FCF5FF
                    ["SurfaceColor"] = Color.FromRgb(254, 250, 255),      // #FEFAFF
                    ["CardBackgroundColor"] = Colors.White,               // #FFFFFF
                    
                    // Text Colors
                    ["TextPrimaryColor"] = Color.FromRgb(74, 20, 140),    // #4A148C
                    ["TextSecondaryColor"] = Color.FromRgb(142, 36, 170),  // #8E24AA
                    ["TextOnPrimaryColor"] = Colors.White,                 // #FFFFFF
                    
                    // Border and Shadow Colors
                    ["BorderColor"] = Color.FromRgb(225, 190, 231),       // #E1BEE7
                    ["ShadowColor"] = Color.FromArgb(30, 123, 31, 162),   // Primary with alpha
                    
                    // Status Colors
                    ["SuccessColor"] = Color.FromRgb(104, 159, 56),       // #689F38
                    ["WarningColor"] = Color.FromRgb(255, 143, 0),        // #FF8F00
                    ["ErrorColor"] = Color.FromRgb(244, 67, 54),          // #F44336
                    ["InfoColor"] = Color.FromRgb(98, 0, 234),            // #6200EA
                }
            };
        }

        public void SaveThemePreference(ThemeType theme)
        {
            try
            {
                Properties.Settings.Default.SelectedTheme = theme.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // Log error if needed
                System.Diagnostics.Debug.WriteLine($"Failed to save theme preference: {ex.Message}");
            }
        }

        public ThemeType LoadThemePreference()
        {
            try
            {
                var savedTheme = Properties.Settings.Default.SelectedTheme;
                if (Enum.TryParse<ThemeType>(savedTheme, out var theme))
                {
                    return theme;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load theme preference: {ex.Message}");
            }
            
            return ThemeType.Light; // Default theme
        }

        public List<ThemeType> GetAvailableThemes()
        {
            return Enum.GetValues<ThemeType>().ToList();
        }
    }
}
