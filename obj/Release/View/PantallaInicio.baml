   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf Q-clr-namespace:Comercial_Estefannny.ConvertersComercial_Estefannny.Converters  G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf .    (Comercial_Estefannny.View.PantallaInicio   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     Dwpf4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     >conv-clr-namespace:Comercial_Estefannny.Converters     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     K
liveCharts4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     Ewfcp4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf       � 	Resources  =   7Comercial_Estefannny.Converters.InverseBooleanConverter   InverseBooleanConverter��%   &	          .��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFFBABAD��� $ 	#FFFDCECF�� � Offset$ 1Q� � RowDefinitions �� $��0* ��� $	��51* ��� $
��108* ��� $	��95* �.+�� $��0,0,10,0q�$	��412��$��Center=�$��2��.+��� $��ALERTA DE STOCK��$��16(�$
��Bold"�$��224,18,1257,0q�$��18��$	��Top=��� -   $��Astock_ListView��$��
10,65,0,10q�$��Visible=�$��Arial Black)�$��11(�$��Visible=�$
��Left=�$	��665�� �� View �� .P��� $��Nombre Producto�� �� Width$	 450�� �� DisplayMemberBinding �� *NombreProducto+�� $��Cantidad��$	 200�� �� *
Cantidad+  LiveCharts.Wpf.PieChart  &   LiveCharts.Wpf.Charts.Base.Chart	   LegendLocation$	 Bottom=�$��856,10,87,10q�
   Series
 �� *ProductCategorySeries+   DataTooltip #  LiveCharts.Wpf.DefaultTooltip     SelectionMode$ 
OnlySenderX��� $��3��$��1q�$��20,10,20,20q�._��� .+��� $��Ganancia por Periodo��$��16(�$
��Bold"�$��Center=�$��	0,10,0,10q���  �� Orientation$ 
Horizontal=�$��Center=�.+�	� $��Diario��$
��10,0q� d� 	IsChecked �� *IsDailyView+	� $��Mensual��$
��10,0q� �� *IsMonthlyView+��  �� SelectedValuePath$	 Tag��$
��10,0q�$	��100�� �� SelectedValue �� *SelectedMonth+ �� UpdateSourceTrigger$ PropertyChanged=�.@��� $��Enero��
 � Tag$ 1���� $��Febrero��$ 2���� $��Marzo��$ 3���� $��Abril��$ 4���� $
��Mayo��$ 5���� $��Junio��$ 6���� $��Julio��$ 7���� $��Agosto��$ 8���� $��
Septiembre��$ 9���� $��Octubre��$ 10���� $��	Noviembre��$ 11���� $��	Diciembre��$ 12��� .+�#  LiveCharts.Wpf.CartesianChart  $��
0,0,0,-232q�
 �� *SeriesCollection1+   AxisX   LiveCharts.Wpf.Axis     Title$ Meses��   Labels �� *Labels+   AxisY   $ Ventas��   LabelFormatter �� *	Formatter+