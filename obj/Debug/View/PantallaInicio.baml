   M S B A M L   `   `   `  ����O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      Q-clr-namespace:Comercial_Estefannny.ConvertersComercial_Estefannny.Converters  5      G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5	      G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5
      G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   .    (Comercial_Estefannny.View.PantallaInicio   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Comercial_Estefannny.View     5      Dwpf4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      >conv-clr-namespace:Comercial_Estefannny.Converters     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5	      K
liveCharts4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5
      Ewfcp4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � 	Resources  5      =   7Comercial_Estefannny.Converters.InverseBooleanConverter   InverseBooleanConverter��%   &	          5   
   6'   5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFFBABAD��6   � 5      $ 	#FFFDCECF��6    � Offset$ 1Q�61   5       � RowDefinitions 5   
   �� 5       $��0* �6   �� 5!      $	��51* �6   �� 5"      $
��108* �6   �� 5$      $	��95* �6   .+�5)   
   � $��0,0,10,0q�6   $	��412��6!   $��Center=�6.   $��2��6I   .+�5*      �� $��ALERTA DE STOCK��6   $��16(�6/   $
��Bold"�6=   $��224,18,1257,0q�5+      $��18��6+   $	��Top=�67   �� 5-      -   5        $��Astock_ListView��5-      $��
10,65,0,10q�5.      $��Visible=�5/      $��Arial Black)�50      $��11(�51      $��Visible=�52      $
��Left=�53      $	��665��6&    �� View 54      �� 55      .P�57      �� $��Nombre Producto��6)    �� Width$	 450��6r    �� DisplayMemberBinding 6B   �� *NombreProducto+6r   �� 5:      $��Cantidad��6)   $	 200��6e    6;   �� *
Cantidad+6e   5;      5=        LiveCharts.Wpf.PieChart  5>      &   LiveCharts.Wpf.Charts.Base.Chart	   LegendLocation$	 Bottom=�6E   $��856,10,87,10q�6]   
   Series
 6   �� *ProductCategorySeries+   DataTooltip 5?      #  LiveCharts.Wpf.DefaultTooltip  5@         SelectionMode$ 
OnlySenderX�6*   5B      5D      �� 5G   
   $��3��6   $��1q�6   $��20,10,20,20q�62   ._�5H      �� .+�5I      �� $��Ganancia por Periodo��6   $��16(�68   $
��Bold"�6F   $��Center=�5J      $��	0,10,0,10q�61   �� 5L       �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5N      	� $��Diario��6"   $
��10,0q�6U    d� 	IsChecked 63   �� *IsDailyView+6U   	� 5O      $��Mensual��6"   $
��10,0q�6X    64   �� *IsMonthlyView+6X   �� 5S       �� SelectedValuePath$	 Tag��5U      $
��10,0q�5V      $	��100��5W       �� SelectedValue 5T      �� *SelectedMonth+ �� UpdateSourceTrigger$ PropertyChanged=�.@�5X      �� $��Enero��6'   
 � Tag$ 1��67   �� 5Y      $��Febrero��6'   $ 2��69   �� 5Z      $��Marzo��6'   $ 3��67   �� 5[      $��Abril��6'   $ 4��67   �� 5\      $
��Mayo��6'   $ 5��66   �� 5]      $��Junio��6'   $ 6��67   �� 5^      $��Julio��6'   $ 7��67   �� 5_      $��Agosto��6'   $ 8��68   �� 5`      $��
Septiembre��6'   $ 9��6<   �� 5a      $��Octubre��6'   $ 10��69   �� 5b      $��	Noviembre��6'   $ 11��6;   �� 5c      $��	Diciembre��6'   $ 12��6;   5d      5g      � 5i      .+�5k      #  LiveCharts.Wpf.CartesianChart  $��
0,0,0,-232q�60   
 6D   �� *SeriesCollection1+   AxisX 5m        LiveCharts.Wpf.Axis     Title$ Meses��6.      Labels 6<   �� *Labels+   AxisY 5p        $ Ventas��6.      LabelFormatter 6=   �� *	Formatter+5r      5s      5x      5y      5z      5{      