   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  O,clr-namespace:Comercial_Estefannny.ViewModelComercial_Estefannny.ViewModel  4    .Comercial_Estefannny.ViewModel.PantallaInicial   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9local'clr-namespace:Comercial_Estefannny.View     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     B	viewModel,clr-namespace:Comercial_Estefannny.ViewModel     X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp    � Title$  PantallaInicial��$
��1920��$
��1080��$
��1280��$	��720��$
��1920��$
��1080�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � AllowsTransparency .  � DataContext 2   ,Comercial_Estefannny.ViewModel.MainViewModel   � 	Resources  �� DataType4   .Comercial_Estefannny.ViewModel.PantallaInicioC.   (Comercial_Estefannny.View.PantallaInicio+   %Comercial_Estefannny.ViewModel.Ventas*   $Comercial_Estefannny.View.VentasView.   (Comercial_Estefannny.ViewModel.ClientesC(   "Comercial_Estefannny.View.Clientes+   %Comercial_Estefannny.ViewModel.Compra'	   !Comercial_Estefannny.View.Compras0
   *Comercial_Estefannny.ViewModel.InventarioC*   $Comercial_Estefannny.View.Inventario0   *Comercial_Estefannny.ViewModel.proveedores+   %Comercial_Estefannny.View.Proveedores3   -Comercial_Estefannny.ViewModel.TransaccionesC-   'Comercial_Estefannny.View.Transacciones0   *Comercial_Estefannny.ViewModel.pruebaBaseC*   $Comercial_Estefannny.View.pruebabase��%  (��       *, +)(��      *, +)(�� $     *, +)(�� 6     *, +)(�� H     *,
 +)(�� Z     *, +)(�� l     *, +)(�� ~     *, +)�� "  .R�  �� "  .R�  �� "  .R�  �� "  .R�	  �� " 
 .R�  �� "  .R�  �� "  .R�  �� "  .R�  .����  �� CornerRadius$ 11��$��2q�����  �� 
StartPoint$	 0,0&�	 �� EndPoint$		 1,1&�.���    winBorderColor1
 � Color#
 [   � Offset$ 0Q��   winBorderColor2#
 [ $	 0.5Q��   winBorderColor3#
 [ $ 1Q�._��  � ColumnDefinitions �� $	��275 ��� .+��� $ 	10,0,0,10��$��0������ $	 0,0&�$		 1,1&�.���   primaryBackColor1#
 [ $ 0Q��   primaryBackColor2#
 [ $	 0.5Q��   primaryBackColor3#
 [ $ 1Q�._��� .+���  �� Orientation$ 
Horizontal=�$��35��$��
10,25,0,40q�.+��� $��/imagenes/screen-cart.png��$��30���� $��Comercial Estefannny��  titleColor1#��[ $��22(�$��
Montserrat)�$��Medium"�$��Center=�$��10,0,0,0q�	�   
menuButton#��[  
 color1
 � Tag# [  d� 	IsChecked$
 TrueY����� *VentanaInicial+.���� $ 
Horizontal=�.+�!  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=� 	 menuButtonIcon#��[	 �� $��Inicio�� 
 menuButtonText#��[
 	� #��[ # [ ���� *VentanaVentas+.���� $ 
Horizontal=�.+�  $ Store=�#��[	 �� $��Ventas��#��[
 	� #��[ # [ ���� *VentanaCompras+.���� $ 
Horizontal=�.+�  $ CartShopping=�#��[	 �� $��Compras��#��[
 	� #��[ # [ ���� *VentanaTransacciones+.���� $ 
Horizontal=�.+�  $ 	Handshake=�#��[	 �� $��Transacciones��#��[
 	� #��[ # [ ���� *VentanaInventario+.���� $ 
Horizontal=�.+�  $ BoxesStacked=�#��[	 �� $��
Inventario��#��[
 	� #��[ # [ ���� *VentanaClientes+.���� $ 
Horizontal=�.+�  $ Users=�#��[	 �� $��Clientes��#��[
 	� #��[ # [ ���� *VentanaProveedores+.���� $ 
Horizontal=�.+�  $ Truck=�#��[	 �� $��Proveedores��#��[
 �� $ 	0,10,10,0��$��1������ $	 0,0&�$		 1,1&�.���   secondaryBackColor1#
 [ $ 0Q��   secondaryBackColor2#
 [ $ 1Q�._��  � RowDefinitions �� $��25 ��� $��35 ��� .+��� -    ��Name$ pnlControlBar��$��0��$ 
Horizontal=�$��RightToLeft=�
������ $��0,0,5,0q�.+��� -    ��Name$ btnClose��  controlButton#��[  
 color4# [ .��  $ RectangleXmark=�  controlButtonIcon#��[ $��21��$��26���� -   $ btnMaximize��#��[  
 color6# [ .��  $ Square=�#��[ �� -   $ btnMinimize��#��[  
 color8# [ .��  $ WindowMinimize=�$��20��#��[ � $��2��.+�(�  (� Stretch$ Uniform=�.��� .+��� ���� *CurrentChildView+�� $
��Auto=�$
��Auto=�.���� ���� *CurrentChildView+