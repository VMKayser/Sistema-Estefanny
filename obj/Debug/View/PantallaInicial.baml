   M S B A M L   `   `   `  ����O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  5   	   O,clr-namespace:Comercial_Estefannny.ViewModelComercial_Estefannny.ViewModel  5   	   4    .Comercial_Estefannny.ViewModel.PantallaInicial   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9local'clr-namespace:Comercial_Estefannny.View     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   B	viewModel,clr-namespace:Comercial_Estefannny.ViewModel     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	     � Title$  PantallaInicial��5
   	   $
��1920��5      $
��1080��6   $
��1280��5      $	��720��6   $
��1920��5      $
��1080��6    � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � DataContext 5      2   ,Comercial_Estefannny.ViewModel.MainViewModel  5   
    � 	Resources 5       �� DataType4   .Comercial_Estefannny.ViewModel.PantallaInicioC.   (Comercial_Estefannny.View.PantallaInicio+   %Comercial_Estefannny.ViewModel.Ventas*   $Comercial_Estefannny.View.VentasView.   (Comercial_Estefannny.ViewModel.ClientesC(   "Comercial_Estefannny.View.Clientes+   %Comercial_Estefannny.ViewModel.Compra'	   !Comercial_Estefannny.View.Compras0
   *Comercial_Estefannny.ViewModel.InventarioC*   $Comercial_Estefannny.View.Inventario0   *Comercial_Estefannny.ViewModel.proveedores+   %Comercial_Estefannny.View.Proveedores3   -Comercial_Estefannny.ViewModel.TransaccionesC-   'Comercial_Estefannny.View.Transacciones0   *Comercial_Estefannny.ViewModel.pruebaBaseC*   $Comercial_Estefannny.View.pruebabase��%X  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5       *, +)(�� �     5#      *,
 +)(�� �     5&      *, +)(�� ,    5)      *, +)(�� ^    5,      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5    
   "  6   .R�5!      	  5"      �� 5#   
   " 
 6   .R�5$        5%      �� 5&   
   "  6   .R�5'        5(      �� 5)   
   "  6   .R�5*        5+      �� 5,   
   "  6   .R�5-        5.      50      .��53      ��  �� CornerRadius$ 11��6   $��2q�54      ��55   
   �� 56       �� 
StartPoint$	 0,0&�6"   	 �� EndPoint$		 1,1&�63   .��57      �    winBorderColor1
 � Color#
 [  6    � Offset$ 0Q�6H   � 58        winBorderColor2#
 [ 6   $	 0.5Q�6H   � 59        winBorderColor3#
 [ 6   $ 1Q�6H   5:      ._�5=   
   �  � ColumnDefinitions 5>      �� 5@      $	��275 �6#   �� 5B      .+�5E      �� $ 	10,0,0,10��6   $��0��5F      ��5G      �� 5H      $	 0,0&�6*   $		 1,1&�6;   .��5I      �   primaryBackColor1#
 [ 6'   $ 0Q�6R   � 5J        primaryBackColor2#
 [ 6'   $	 0.5Q�6R   � 5K        primaryBackColor3#
 [ 6'   $ 1Q�6R   5L      ._�5O      �� .+�5Q      ��  �� Orientation$ 
Horizontal=�6!   $��35��5R   !   $��
10,25,0,40q�5S   !   .+�5T      �� $��/imagenes/screen-cart.png��6    $��30��6C   �� 5U      $��Comercial Estefannny��6$     titleColor1#��[ 5V   $   $��22(�5W   $   $��
Montserrat)�5X   $   $��Medium"�5Y   $   $��Center=�5Z   $   $��10,0,0,0q�5[   $   5\      	� 5^        
menuButton#��[ 6"    
 color1
 � Tag# [ 5_   "    d� 	IsChecked$
 TrueY�5a   "   ��5b   "   �� *VentanaInicial+.��5c      �� $ 
Horizontal=�6%   .+�5d      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$
 Home=�6+    	 menuButtonIcon#��[	 67   �� 5e      $��Inicio��6(    
 menuButtonText#��[
 66   5f      5g      	� 5r      #��[ 6"   # [ 5s   "   ��5t   "   �� *VentanaVentas+.��5u      �� $ 
Horizontal=�6%   .+�5v        $ Store=�6+   #��[	 68   �� 5w      $��Ventas��6(   #��[
 66   5x      5y      	� 5{      #��[ 6"   # [ 5|   !   ��5}   !   �� *VentanaCompras+.��5~      �� $ 
Horizontal=�6%   .+�5        $ CartShopping=�6+   #��[	 6?   �� 5�      $��Compras��6(   #��[
 67   5�      5�      	� 5�      #��[ 6"   # [ 5�   !   ��5�   !   �� *VentanaTransacciones+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ 	Handshake=�6+   #��[	 6<   �� 5�      $��Transacciones��6(   #��[
 6=   5�      5�      	� 5�      #��[ 6"   # [ 5�   !   ��5�   !   �� *VentanaInventario+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ BoxesStacked=�6+   #��[	 6?   �� 5�      $��
Inventario��6(   #��[
 6:   5�      5�      	� 5�      #��[ 6"   # [ 5�   !   ��5�   !   �� *VentanaClientes+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ Users=�6+   #��[	 68   �� 5�      $��Clientes��6(   #��[
 68   5�      5�      	� 5�      #��[ 6"   # [ 5�   !   ��5�   !   �� *VentanaProveedores+.��5�      �� $ 
Horizontal=�6%   .+�5�        $ Truck=�6+   #��[	 68   �� 5�      $��Proveedores��6(   #��[
 6;   5�      5�      5�      5�      �� 5�      $ 	0,10,10,0��6   $��1��5�      ��5�      �� 5�      $	 0,0&�6*   $		 1,1&�6;   .��5�      �   secondaryBackColor1#
 [ 6'   $ 0Q�6T   � 5�        secondaryBackColor2#
 [ 6'   $ 1Q�6T   5�      ._�5�      �  � RowDefinitions 5�      �� 5�      $��25 �6(   �� 5�      $��35 �6(   �� 5�      .+�5�      �� -   5         ��Name$ pnlControlBar��5�   !   $��0��5�   !   $ 
Horizontal=�5�   !   $��RightToLeft=�5�   !   
������ $��0,0,5,0q�5�   !   .+�5�      �� -   5         ��Name$ btnClose��5�   !     controlButton#��[ 5�       
 color4# [ 5�      .��5�        $ RectangleXmark=�6+     controlButtonIcon#��[ 6A   $��21��6l   $��26��6x   5�      �� 5�      -   5        $ btnMaximize��5�   !   #��[ 5�   !    
 color6# [ 5�   !   .��5�        $ Square=�6+   #��[ 69   5�      �� 5�      -   5        $ btnMinimize��5�   !   #��[ 5�   !    
 color8# [ 5�   !   .��5�        $ WindowMinimize=�6+   $��20��6A   #��[ 6L   5�      5�      � 5�      $��2��6   .+�5�      (�  (� Stretch$ Uniform=�6"   .��5�      � .+�5�   "   �� ��61   �� *CurrentChildView+5�      5�      �� 5�      $
��Auto=�6'   $
��Auto=�6J   .��5�      �� ��6-   �� *CurrentChildView+5�      5�      5     5     5     5     5     