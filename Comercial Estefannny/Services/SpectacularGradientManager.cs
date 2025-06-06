using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Comercial_Estefannny.Services
{
    /// <summary>
    /// 🎨 Spectacular Gradient Manager - Sistema de gestión de gradientes espectaculares
    /// Maneja los efectos visuales, animaciones y persistencia de gradientes
    /// </summary>
    public class SpectacularGradientManager
    {
        private static SpectacularGradientManager? _instance;
        private string _currentGradient = "SunsetFresa";
        private bool _enableSpectacularEffects = true;
        private double _glowIntensity = 0.8;
        
        // Constructor público para permitir instanciación directa
        public SpectacularGradientManager()
        {
            LoadGradientSettings();
        }
        
        public static SpectacularGradientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpectacularGradientManager();
                }
                return _instance;
            }
        }

        // Event handler for gradient changes
        public event EventHandler<GradientChangedEventArgs>? GradientChanged;

        /// <summary>
        /// 🌟 Obtiene la colección completa de gradientes espectaculares disponibles
        /// </summary>
        public Dictionary<string, GradientInfo> GetAvailableGradients()
        {
            return new Dictionary<string, GradientInfo>
            {
                // Original Spectacular Collection
                { "SunsetFresa", new GradientInfo("🍓 Sunset Fresa", "Sunset", "#FF6B6B to #4ECDC4") },
                { "SunsetMagenta", new GradientInfo("🌺 Sunset Magenta", "Sunset", "#E91E63 to #FFE082") },
                { "SunsetFucsia", new GradientInfo("🌸 Sunset Fucsia", "Sunset", "#E040FB to #FF6EC7") },
                { "OceanTurquesa", new GradientInfo("🏝️ Ocean Turquesa", "Ocean", "#1DE9B6 to #00ACC1") },
                { "OceanBlue", new GradientInfo("🌊 Ocean Blue", "Ocean", "#2196F3 to #00BCD4") },
                { "RainbowDegradation", new GradientInfo("🌈 Rainbow", "Effects", "Multi-color rainbow") },
                { "SunsetToOcean", new GradientInfo("🌅🌊 Sunset Ocean", "Effects", "Sunset to Ocean transition") },
                { "RadialGlow", new GradientInfo("💫 Radial Glow", "Effects", "Radial glow effect") },
                
                // Exclusive Premium Collection
                { "AuroraBorealis", new GradientInfo("🌌 Aurora Borealis", "Exclusive", "Cosmic northern lights") },
                { "CosmicGalaxy", new GradientInfo("🌌🌟 Cosmic Galaxy", "Exclusive", "Deep space galaxy") },
                { "NeonCyber", new GradientInfo("⚡🔮 Neon Cyber", "Exclusive", "Cyberpunk neon effects") },
                { "EmeraldFire", new GradientInfo("💎🔥 Emerald Fire", "Exclusive", "Green to orange fire") },
                { "RoyalPurple", new GradientInfo("👑💜 Royal Purple", "Exclusive", "Elegant purple radial") },
                { "Holographic", new GradientInfo("🌈✨ Holographic", "Exclusive", "Rainbow holographic") }
            };
        }

        /// <summary>
        /// 🎯 Aplica un gradiente específico y sus efectos
        /// </summary>
        public void ApplyGradient(string gradientName)
        {
            try
            {
                if (string.IsNullOrEmpty(gradientName))
                    return;

                // Validar si el gradiente existe
                var availableGradients = GetAvailableGradients();
                if (!availableGradients.ContainsKey(gradientName))
                {
                    throw new ArgumentException($"Gradient '{gradientName}' not found in available gradients.");
                }

                _currentGradient = gradientName;
                
                // Aplicar efectos espectaculares si están habilitados
                if (_enableSpectacularEffects)
                {
                    ApplySpectacularEffects(gradientName);
                }

                // Guardar preferencia
                SaveGradientPreference(gradientName);
                
                // Notificar cambio
                GradientChanged?.Invoke(this, new GradientChangedEventArgs(gradientName));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error applying gradient '{gradientName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// ✨ Aplica efectos espectaculares específicos para cada gradiente
        /// </summary>
        private void ApplySpectacularEffects(string gradientName)
        {
            try
            {
                var application = Application.Current;
                if (application?.Resources == null) return;

                // Aplicar efectos específicos según el gradiente
                switch (gradientName)
                {
                    case "AuroraBorealis":
                    case "CosmicGalaxy":
                        ApplyCosmicEffects();
                        break;
                    case "NeonCyber":
                        ApplyNeonEffects();
                        break;
                    case "HolographicGradient":
                        ApplyHolographicEffects();
                        break;
                    case "RainbowDegradation":
                        ApplyRainbowEffects();
                        break;
                    default:
                        ApplyStandardEffects();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw to avoid breaking the application
                System.Diagnostics.Debug.WriteLine($"Error applying spectacular effects: {ex.Message}");
            }
        }

        /// <summary>
        /// 🌌 Efectos cósmicos para gradientes espaciales
        /// </summary>
        private void ApplyCosmicEffects()
        {
            // Implementar efectos de pulsación cósmica
            var pulseAnimation = new DoubleAnimation
            {
                From = 0.7,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
        }

        /// <summary>
        /// ⚡ Efectos neón para temas cyberpunk
        /// </summary>
        private void ApplyNeonEffects()
        {
            // Implementar efectos de parpadeo neón
            var glowAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
        }

        /// <summary>
        /// 🌈 Efectos holográficos multicolor
        /// </summary>
        private void ApplyHolographicEffects()
        {
            // Implementar rotación de color holográfica
            var rotationAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever
            };
        }

        /// <summary>
        /// 🌈 Efectos arcoíris en movimiento
        /// </summary>
        private void ApplyRainbowEffects()
        {
            // Implementar animación de arcoíris
            var rainbowAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever
            };
        }

        /// <summary>
        /// ✨ Efectos estándar para gradientes básicos
        /// </summary>
        private void ApplyStandardEffects()
        {
            // Efectos sutiles para gradientes estándar
            var subtleAnimation = new DoubleAnimation
            {
                From = 0.9,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
        }

        /// <summary>
        /// 💾 Carga la configuración de gradientes desde App.config
        /// </summary>
        private void LoadGradientSettings()
        {
            try
            {
                _currentGradient = ConfigurationManager.AppSettings["SelectedGradient"] ?? "SunsetFresa";
                _enableSpectacularEffects = bool.Parse(ConfigurationManager.AppSettings["EnableSpectacularEffects"] ?? "true");
                _glowIntensity = double.Parse(ConfigurationManager.AppSettings["GlowIntensity"] ?? "0.8");
            }
            catch (Exception ex)
            {
                // Use defaults if configuration loading fails
                System.Diagnostics.Debug.WriteLine($"Error loading gradient settings: {ex.Message}");
                _currentGradient = "SunsetFresa";
                _enableSpectacularEffects = true;
                _glowIntensity = 0.8;
            }
        }

        /// <summary>
        /// 💾 Guarda la preferencia de gradiente en App.config
        /// </summary>
        private void SaveGradientPreference(string gradientName)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                
                if (config.AppSettings.Settings["SelectedGradient"] != null)
                {
                    config.AppSettings.Settings["SelectedGradient"].Value = gradientName;
                }
                else
                {
                    config.AppSettings.Settings.Add("SelectedGradient", gradientName);
                }

                if (config.AppSettings.Settings["LastSelectedGradient"] != null)
                {
                    config.AppSettings.Settings["LastSelectedGradient"].Value = gradientName;
                }
                else
                {
                    config.AppSettings.Settings.Add("LastSelectedGradient", gradientName);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                // Log error but don't throw to avoid breaking the application
                System.Diagnostics.Debug.WriteLine($"Error saving gradient preference: {ex.Message}");
            }
        }

        /// <summary>
        /// 🎯 Obtiene el gradiente actualmente seleccionado
        /// </summary>
        public string CurrentGradient => _currentGradient;

        /// <summary>
        /// ✨ Indica si los efectos espectaculares están habilitados
        /// </summary>
        public bool SpectacularEffectsEnabled => _enableSpectacularEffects;

        /// <summary>
        /// 🌟 Intensidad del brillo (0.0 - 1.0)
        /// </summary>
        public double GlowIntensity => _glowIntensity;

        /// <summary>
        /// 🔧 Configura los efectos espectaculares
        /// </summary>
        public void ConfigureSpectacularEffects(bool enabled, double glowIntensity = 0.8)
        {
            _enableSpectacularEffects = enabled;
            _glowIntensity = Math.Max(0.0, Math.Min(1.0, glowIntensity));
        }
    }

    /// <summary>
    /// 📊 Información sobre un gradiente específico
    /// </summary>
    public class GradientInfo
    {
        public string DisplayName { get; }
        public string Category { get; }
        public string Description { get; }

        public GradientInfo(string displayName, string category, string description)
        {
            DisplayName = displayName;
            Category = category;
            Description = description;
        }
    }

    /// <summary>
    /// 🎯 Argumentos del evento de cambio de gradiente
    /// </summary>
    public class GradientChangedEventArgs : EventArgs
    {
        public string GradientName { get; }

        public GradientChangedEventArgs(string gradientName)
        {
            GradientName = gradientName;
        }
    }
}
