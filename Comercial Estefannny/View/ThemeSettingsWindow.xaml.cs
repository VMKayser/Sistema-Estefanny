using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Configuration;
using Comercial_Estefannny.Services;

namespace Comercial_Estefannny.View
{
    public partial class ThemeSettingsWindow : Window
    {
        private ThemeType _originalTheme;
        private ThemeType _selectedTheme;
        private string _selectedGradient = ""; // Para almacenar el gradiente seleccionado
        private string _originalGradient = ""; // Para restaurar el gradiente original

        public ThemeSettingsWindow()
        {
            InitializeComponent();
            InitializeThemeSettings();
            InitializeSpectacularEffects();
        }

        private void InitializeThemeSettings()
        {
            _originalTheme = ThemeManager.Instance.CurrentTheme;
            _selectedTheme = _originalTheme;
            
            UpdateCurrentThemeDisplay();
            HighlightSelectedTheme();
        }

        private void UpdateCurrentThemeDisplay()
        {
            var themeName = GetThemeDisplayName(_selectedTheme);
            CurrentThemeText.Text = $"Tema actual: {themeName}";
        }

        private string GetThemeDisplayName(ThemeType theme)
        {
            return theme switch
            {
                ThemeType.Light => "Claro",
                ThemeType.Dark => "Oscuro",
                ThemeType.Blue => "Azul",
                ThemeType.Green => "Verde",
                ThemeType.Purple => "Púrpura",
                _ => "Desconocido"
            };
        }

        private void HighlightSelectedTheme()
        {
            // Reset all theme options
            foreach (Border themeOption in ThemeGrid.Children.OfType<Border>())
            {
                themeOption.BorderBrush = new SolidColorBrush(Color.FromRgb(222, 226, 230)); // #DEE2E6
                themeOption.BorderThickness = new Thickness(1);
                
                // Remove previous selection indicators
                var grid = themeOption.Child as StackPanel;
                if (grid != null)
                {
                    // Remove any existing selection indicator
                    var existingIndicator = grid.Children.OfType<Border>()
                        .FirstOrDefault(b => b.Name == "SelectionIndicator");
                    if (existingIndicator != null)
                    {
                        grid.Children.Remove(existingIndicator);
                    }
                }
            }

            // Highlight selected theme
            var selectedOption = ThemeGrid.Children.OfType<Border>()
                .FirstOrDefault(b => b.Tag.ToString() == _selectedTheme.ToString());
            
            if (selectedOption != null)
            {
                selectedOption.BorderBrush = new SolidColorBrush(Color.FromRgb(52, 152, 219)); // Primary color
                selectedOption.BorderThickness = new Thickness(3);
                
                // Add selection indicator
                var stackPanel = selectedOption.Child as StackPanel;
                if (stackPanel != null)
                {
                    var indicator = new Border
                    {
                        Name = "SelectionIndicator",
                        Background = new SolidColorBrush(Color.FromRgb(52, 152, 219)),
                        Height = 4,
                        CornerRadius = new CornerRadius(2),
                        Margin = new Thickness(10, 5, 10, 0)
                    };
                    stackPanel.Children.Add(indicator);
                }
            }
        }

        private void ThemeOption_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag != null)
            {
                if (Enum.TryParse<ThemeType>(border.Tag.ToString(), out var theme))
                {
                    _selectedTheme = theme;
                    
                    // Apply theme immediately for preview
                    ThemeManager.Instance.SetTheme(_selectedTheme);
                    
                    UpdateCurrentThemeDisplay();
                    HighlightSelectedTheme();
                }
            }
        }        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the theme preference
            ThemeManager.Instance.SaveThemePreference(_selectedTheme);
            
            // Guardar también el gradiente seleccionado si hay uno
            if (!string.IsNullOrEmpty(_selectedGradient))
            {
                // Aplicar el gradiente seleccionado permanentemente
                var gradientBrush = GetGradientBrush(_selectedGradient);
                if (gradientBrush != null)
                {
                    UpdateApplicationGradients(gradientBrush, _selectedGradient);
                }
            }
            
            ShowSuccessMessage();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Restore original theme
            ThemeManager.Instance.SetTheme(_originalTheme);
            
            DialogResult = false;
            Close();
        }        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedTheme = ThemeType.Light;
            _selectedGradient = "";
            ThemeManager.Instance.SetTheme(_selectedTheme);
            
            // Resetear gradientes a los predeterminados
            Application.Current.Resources.Remove("PrimaryGradient");
            Application.Current.Resources.Remove("AccentGradient");
            Application.Current.Resources.Remove("ButtonHoverEffect");
            Application.Current.Resources.Remove("GlowEffect");
            Application.Current.Resources.Remove("TransitionGradient");
            
            UpdateCurrentThemeDisplay();
            HighlightSelectedTheme();
            RemoveGradientHighlight();
            
            ShowInfoMessage("Tema y gradientes restablecidos al predeterminado");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CancelButton_Click(sender, e);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ShowSuccessMessage()
        {
            MessageBox.Show(
                "El tema se ha aplicado correctamente.\n\nLos cambios se mantendrán la próxima vez que abras la aplicación.",
                "Tema Aplicado",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(
                message,
                "Información",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }        /// <summary>
        /// Maneja la selección de gradientes espectaculares
        /// </summary>
        private void GradientOption_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag != null)
            {
                var gradientName = border.Tag.ToString();
                if (!string.IsNullOrEmpty(gradientName))
                {
                    _selectedGradient = gradientName;
                    
                    // Aplicar el gradiente seleccionado inmediatamente para vista previa
                    ApplySpectacularGradient(gradientName);
                    
                    // Aplicar animaciones espectaculares
                    ApplySpectacularAnimation(border, gradientName);
                    
                    // Actualizar la vista previa
                    UpdateGradientPreview(gradientName);
                    
                    // Resaltar el gradiente seleccionado
                    HighlightSelectedGradient(border);
                    
                    ShowInfoMessage($"Gradiente '{GetGradientDisplayName(gradientName)}' aplicado para vista previa");
                }
            }
        }
        
        /// <summary>
        /// Aplica un gradiente espectacular al tema actual
        /// </summary>
        private void ApplySpectacularGradient(string gradientName)
        {
            try
            {
                // Obtener el brush del gradiente seleccionado
                var gradientBrush = GetGradientBrush(gradientName);
                
                if (gradientBrush != null)
                {
                    // Aplicar el gradiente a elementos específicos de la aplicación
                    UpdateApplicationGradients(gradientBrush, gradientName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el gradiente: {ex.Message}", 
                               "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
          /// <summary>
        /// Obtiene el brush del gradiente por su nombre
        /// </summary>
        private Brush? GetGradientBrush(string gradientName)
        {
            try
            {
                return gradientName switch
                {
                    "SunsetFresa" => (Brush)FindResource("SunsetFresaGradient"),
                    "SunsetMagenta" => (Brush)FindResource("SunsetMagentaGradient"),
                    "SunsetFucsia" => (Brush)FindResource("SunsetFucsiaGradient"),
                    "OceanTurquesa" => (Brush)FindResource("OceanTurquesaGradient"),
                    "OceanBlue" => (Brush)FindResource("OceanBlueGradient"),
                    "RainbowDegradation" => (Brush)FindResource("RainbowDegradation"),
                    "SunsetToOcean" => (Brush)FindResource("SunsetToOcean"),
                    "RadialGlow" => (Brush)FindResource("RadialFresaGlow"),
                    "AuroraBorealis" => (Brush)FindResource("AuroraBorealisGradient"),
                    "CosmicGalaxy" => (Brush)FindResource("CosmicGalaxyGradient"),
                    "NeonCyber" => (Brush)FindResource("NeonCyberGradient"),
                    "EmeraldFire" => (Brush)FindResource("EmeraldFireGradient"),
                    "RoyalPurple" => (Brush)FindResource("RoyalPurpleGradient"),
                    "Holographic" => (Brush)FindResource("HolographicGradient"),
                    _ => null
                };
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// Actualiza los gradientes en la aplicación
        /// </summary>
        private void UpdateApplicationGradients(Brush gradientBrush, string gradientName)
        {
            // Actualizar el ResourceDictionary principal
            Application.Current.Resources["PrimaryGradient"] = gradientBrush;
            Application.Current.Resources["AccentGradient"] = gradientBrush;
            
            // Aplicar efectos específicos según el tipo de gradiente
            ApplyGradientEffects(gradientName);
        }
        
        /// <summary>
        /// Aplica efectos visuales específicos según el gradiente
        /// </summary>
        private void ApplyGradientEffects(string gradientName)
        {
            try
            {
                switch (gradientName)
                {
                    case "RainbowDegradation":
                        // Aplicar efectos de arcoíris
                        if (FindResource("RainbowBorder") is Brush rainbowBrush)
                            Application.Current.Resources["ButtonHoverEffect"] = rainbowBrush;
                        break;
                        
                    case "RadialGlow":
                        // Aplicar efectos de resplandor
                        if (FindResource("RadialFresaGlow") is Brush glowBrush)
                            Application.Current.Resources["GlowEffect"] = glowBrush;
                        break;
                        
                    case "SunsetToOcean":
                        // Aplicar transición de colores
                        if (FindResource("SunsetToOcean") is Brush transitionBrush)
                            Application.Current.Resources["TransitionGradient"] = transitionBrush;
                        break;
                }
            }
            catch
            {
                // Ignorar errores de recursos no encontrados
            }
        }
        
        /// <summary>
        /// Actualiza la vista previa del gradiente seleccionado
        /// </summary>
        private void UpdateGradientPreview(string gradientName)
        {
            // Encontrar el panel de vista previa y actualizar su fondo
            if (FindName("PreviewPanel") is Border previewPanel)
            {
                var gradientBrush = GetGradientBrush(gradientName);
                if (gradientBrush != null)
                {
                    previewPanel.Background = gradientBrush;
                }
            }
        }
        
        /// <summary>
        /// Resalta el gradiente seleccionado visualmente
        /// </summary>
        private void HighlightSelectedGradient(Border selectedBorder)
        {
            // Remover el resaltado de todos los gradientes
            RemoveGradientHighlight();
            
            // Agregar resaltado al seleccionado
            try
            {
                if (FindResource("RainbowBorder") is Brush rainbowBrush)
                {
                    selectedBorder.BorderBrush = rainbowBrush;
                    selectedBorder.BorderThickness = new Thickness(3);
                }
                else
                {
                    selectedBorder.BorderBrush = Brushes.Gold;
                    selectedBorder.BorderThickness = new Thickness(3);
                }
                
                // Aplicar animación de selección
                var scaleTransform = new ScaleTransform(1.1, 1.1);
                selectedBorder.RenderTransform = scaleTransform;
            }
            catch
            {
                // Usar color por defecto si no se encuentra el recurso
                selectedBorder.BorderBrush = Brushes.Gold;
                selectedBorder.BorderThickness = new Thickness(3);
            }
        }
        
        /// <summary>
        /// Remueve el resaltado de todos los gradientes
        /// </summary>
        private void RemoveGradientHighlight()
        {
            // Buscar todos los bordes de gradientes en las pestañas
            if (FindName("GradientTabControl") is TabControl tabControl)
            {
                foreach (TabItem tab in tabControl.Items)
                {
                    if (tab.Content is ScrollViewer scrollViewer && 
                        scrollViewer.Content is WrapPanel wrapPanel)
                    {
                        foreach (Border border in wrapPanel.Children.OfType<Border>())
                        {
                            border.BorderThickness = new Thickness(0);
                            border.RenderTransform = new ScaleTransform(1.0, 1.0);
                        }
                    }
                }
            }
        }
          /// <summary>
        /// Obtiene el nombre de visualización del gradiente
        /// </summary>
        private string GetGradientDisplayName(string gradientName)
        {
            return gradientName switch
            {
                "SunsetFresa" => "Atardecer Fresa",
                "SunsetMagenta" => "Atardecer Magenta",
                "SunsetFucsia" => "Atardecer Fucsia",
                "OceanTurquesa" => "Océano Turquesa",
                "OceanBlue" => "Océano Azul",
                "RainbowDegradation" => "Degradado Arcoíris",
                "SunsetToOcean" => "Atardecer a Océano",
                "RadialGlow" => "Resplandor Radial",
                "AuroraBorealis" => "Aurora Boreal",
                "CosmicGalaxy" => "Galaxia Cósmica",
                "NeonCyber" => "Neón Cyber",
                "EmeraldFire" => "Fuego Esmeralda",
                "RoyalPurple" => "Púrpura Real",
                "Holographic" => "Holográfico",
                _ => gradientName
            };
        }
        
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            
            // Apply window shadow effect if available
            try
            {
                var helper = new System.Windows.Interop.WindowInteropHelper(this);
                // Add drop shadow effect
            }
            catch
            {
                // Ignore if shadow effect is not available
            }
        }
        
        /// <summary>
        /// Inicializa efectos espectaculares para la ventana
        /// </summary>
        private void InitializeSpectacularEffects()
        {
            try
            {
                // Aplicar animación de entrada a la ventana
                var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
                this.BeginAnimation(OpacityProperty, fadeInAnimation);
                
                // Inicializar transformaciones para animaciones
                this.RenderTransform = new ScaleTransform(0.95, 0.95);
                this.RenderTransformOrigin = new Point(0.5, 0.5);
                
                var scaleAnimation = new DoubleAnimation(0.95, 1.0, TimeSpan.FromSeconds(0.3));
                var scaleTransform = this.RenderTransform as ScaleTransform;
                scaleTransform?.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                scaleTransform?.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                
                // Cargar gradiente guardado si existe
                LoadSavedGradient();
                
                // Inicializar efectos de resplandor para el panel de vista previa
                InitializePreviewPanelEffects();
            }
            catch (Exception ex)
            {
                // Log del error sin interrumpir la inicialización
                System.Diagnostics.Debug.WriteLine($"Error initializing spectacular effects: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Inicializa efectos del panel de vista previa
        /// </summary>
        private void InitializePreviewPanelEffects()
        {
            try
            {
                if (FindName("PreviewPanel") is Border previewPanel)
                {
                    // Aplicar efecto de resplandor inicial
                    var glowEffect = new DropShadowEffect
                    {
                        Color = Color.FromRgb(255, 107, 117), // Color base del gradiente Fresa
                        BlurRadius = 20,
                        ShadowDepth = 0,
                        Opacity = 0.7
                    };
                    previewPanel.Effect = glowEffect;
                    
                    // Animación de resplandor pulsante
                    var glowAnimation = new DoubleAnimation(15, 25, TimeSpan.FromSeconds(2))
                    {
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    glowEffect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, glowAnimation);
                }
            }
            catch
            {
                // Ignorar errores de efectos visuales
            }
        }
        
        /// <summary>
        /// Carga el gradiente guardado de las configuraciones
        /// </summary>
        private void LoadSavedGradient()
        {
            try
            {
                // Intentar cargar desde configuración de aplicación
                var savedGradient = ConfigurationManager.AppSettings["SelectedGradient"];
                if (!string.IsNullOrEmpty(savedGradient))
                {
                    _selectedGradient = savedGradient;
                    _originalGradient = savedGradient;
                    
                    // Aplicar el gradiente guardado
                    ApplySpectacularGradient(savedGradient);
                    
                    // Resaltar el gradiente en la UI
                    HighlightSavedGradient(savedGradient);
                }
            }
            catch
            {
                // Si no se puede cargar, usar gradiente por defecto
                _selectedGradient = "SunsetFresa";
                _originalGradient = "SunsetFresa";
            }
        }
        
        /// <summary>
        /// Resalta el gradiente guardado en la interfaz
        /// </summary>
        private void HighlightSavedGradient(string gradientName)
        {
            try
            {
                if (FindName("GradientTabControl") is TabControl tabControl)
                {
                    foreach (TabItem tab in tabControl.Items)
                    {
                        if (tab.Content is ScrollViewer scrollViewer && 
                            scrollViewer.Content is WrapPanel wrapPanel)
                        {                            var gradientBorder = wrapPanel.Children.OfType<Border>()
                                .FirstOrDefault(b => b.Tag?.ToString() == gradientName);
                            
                            if (gradientBorder != null)
                            {
                                HighlightSelectedGradient(gradientBorder);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignorar errores de UI
            }
        }
        
        /// <summary>
        /// Guarda las preferencias de gradiente
        /// </summary>
        private void SaveGradientPreference(string gradientName)
        {
            try
            {
                // Usar Properties.Settings para persistir la configuración
                Properties.Settings.Default["SelectedGradient"] = gradientName;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Ignorar errores de persistencia
            }
        }
        
        /// <summary>
        /// Aplica animaciones espectaculares al gradiente seleccionado
        /// </summary>
        private void ApplySpectacularAnimation(Border gradientBorder, string gradientName)
        {
            try
            {
                // Inicializar transformación si no existe
                if (gradientBorder.RenderTransform == null)
                {
                    gradientBorder.RenderTransform = new ScaleTransform(1.0, 1.0);
                    gradientBorder.RenderTransformOrigin = new Point(0.5, 0.5);
                }
                
                // Animación de selección con efecto rebote
                var scaleAnimation = new DoubleAnimation(1.0, 1.15, TimeSpan.FromSeconds(0.15))
                {
                    AutoReverse = true,
                    RepeatBehavior = new RepeatBehavior(2)
                };
                
                if (gradientBorder.RenderTransform is ScaleTransform transform)
                {
                    transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                    transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                }
                
                // Efecto de resplandor mejorado según el tipo de gradiente
                ApplyContextualGlow(gradientBorder, gradientName);
            }
            catch
            {
                // Usar animación básica si fallan las avanzadas
                try
                {
                    var basicScale = new DoubleAnimation(1.0, 1.1, TimeSpan.FromSeconds(0.2));
                    if (gradientBorder.RenderTransform is ScaleTransform transform)
                    {
                        transform.BeginAnimation(ScaleTransform.ScaleXProperty, basicScale);
                        transform.BeginAnimation(ScaleTransform.ScaleYProperty, basicScale);
                    }
                }
                catch
                {
                    // Ignorar si no se pueden aplicar animaciones
                }
            }
        }
          /// <summary>
        /// Aplica efectos de resplandor contextual según el gradiente
        /// </summary>
        private void ApplyContextualGlow(Border border, string gradientName)
        {
            try
            {
                Color glowColor = gradientName switch
                {
                    "SunsetFresa" => Color.FromRgb(255, 107, 117),
                    "SunsetMagenta" => Color.FromRgb(233, 30, 99),
                    "SunsetFucsia" => Color.FromRgb(224, 64, 251),
                    "OceanTurquesa" => Color.FromRgb(29, 233, 182),
                    "OceanBlue" => Color.FromRgb(33, 150, 243),
                    "RainbowDegradation" => Color.FromRgb(148, 0, 211),
                    "SunsetToOcean" => Color.FromRgb(255, 107, 117),
                    "RadialGlow" => Color.FromRgb(255, 107, 117),
                    "AuroraBorealis" => Color.FromRgb(0, 201, 255),
                    "CosmicGalaxy" => Color.FromRgb(102, 126, 234),
                    "NeonCyber" => Color.FromRgb(255, 0, 128),
                    "EmeraldFire" => Color.FromRgb(17, 153, 142),
                    "RoyalPurple" => Color.FromRgb(131, 96, 195),
                    "Holographic" => Color.FromRgb(255, 154, 158),
                    _ => Color.FromRgb(52, 152, 219)
                };
                
                var glowEffect = new DropShadowEffect
                {
                    Color = glowColor,
                    BlurRadius = 25,
                    ShadowDepth = 0,
                    Opacity = 0.8
                };
                
                border.Effect = glowEffect;
            }
            catch
            {
                // Ignorar errores de efectos
            }
        }        private void TestGradientsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Spectacular Gradient System integration
                var gradientManager = Services.SpectacularGradientManager.Instance;
                
                // Show current gradient collection info
                string currentGradient = gradientManager.CurrentGradient;
                int totalGradients = gradientManager.GetAvailableGradients().Count;
                bool effectsEnabled = gradientManager.SpectacularEffectsEnabled;
                double glowIntensity = gradientManager.GlowIntensity;
                
                MessageBox.Show($"🎨 Spectacular Gradient System Active!\n\n" +
                               $"✨ Current Gradient: {currentGradient}\n" +
                               $"🌈 Total Gradients Available: {totalGradients}\n" +
                               $"🔥 Spectacular Effects: {(effectsEnabled ? "Enabled" : "Disabled")}\n" +
                               $"💫 Glow Intensity: {glowIntensity:P0}\n\n" +
                               $"🌟 Collections: Sunset, Ocean, Effects, Exclusive\n" +
                               $"✨ Special Effects: Cosmic, Neon, Holographic\n\n" +
                               $"Switch to different tabs to see gradients in action!",
                               "🎨 Gradient System Status", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing gradient system: {ex.Message}", 
                               "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
