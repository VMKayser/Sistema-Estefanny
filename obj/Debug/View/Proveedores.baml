   M S B A M L   `   `   `  ����O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  5      +    %Comercial_Estefannny.View.Proveedores   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Comercial_Estefannny.View     5      X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5
      � .+�5   
   �   � RowDefinitions  5      �� 5      $	��51* �6    �� 5      $	��50* �6     � ColumnDefinitions 5      �� 5      $��* �6#   .+�5      � $��1��6    5      �� 5      .+�5      �� -   5         ��Name$ ProveedoresListView��5      $��10,23,10,10q�68   $��Visible=�6M   $��Arial Black)�6�   $��11(�6�   $��Visible=�6�    �� View 5      �� 5      .P�5      �� $��Nombre��6-    �� Width$	 300��6e    �� DisplayMemberBinding 6=   �� *Nombre+6e   �� 5      $��Telefono��6-   $	 150��6i    6?   �� *
Telefono+6i   �� 5      $��	Direccion��6-   $	 150��6k    6@   �� *	Direccion+6k   5      5!      �� 5"      -   5         ��Name$ TextBuscadorProveedor��5"      $
��Left=�69   $��10,0,0,0q�6T    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��171��6�   $��18��6�   5#      � 5$      $��	0,1,375,0q�6     5%      �� 5&      �� 5'      �� 5(      �� 5)      .+�5+      �� -   5        $ NombreProveedor��5+      $
��Left=�63   $��10,0,0,0q�6N   $
 Wrap=�6`   $��NOMBRE:��6t   $��Center=�6�   $��92��6�   $��Arial Black)�6�   $��16(�6�    J� 	AllowDrop . 	 �� 
IsReadOnly	 . $��20��6:  ��6�   Z� ��6	  Z� 
 �� SelectionBrush
 6   Z� 6:  �� 5,      -   5        $ DireccionProveedor��5,      $
��Left=�66   $��10,0,0,0q�6Q   $
 Wrap=�6c   $��
DIRECCION:��6w   $��Center=�6�   $	��118��6�   $��Arial Black)�6�   $��16(�6�    . 	 . $��2��6A  $��20��6N  ��6�   Z� ��6  Z� 
 6'  Z� 6N  �� 5-      -   5        $ TelefonoProveedor��5-      $
��Left=�65   $��10,0,0,0q�6P   $
 Wrap=�6b   $��	TELEFONO:��6v   $��Center=�6�   $	��118��6�   $��Arial Black)�6�   $��16(�6�    . 	 . $��1��6?  $��20��6L  ��6�   Z� ��6  Z� 
 6%  Z� 6L  �� 5.      -   5        $ TextDireccionProveedor��5.      $
��Left=�6:   $��	210,0,0,0q�6U   $��2��6h   $
 Wrap=�6u   $��Center=�6�   $	��147��6�   $��22��6�   �� 5/      -   5        $ TextTelefonoProveedor��5/      $
��Left=�69   $��	210,0,0,0q�6T   $��1��6g   $
 Wrap=�6t   $��Center=�6�   $	��147��6�   $��22��6�   �� 50      -   5        $ TextNombreProveedor��50      $
��Left=�67   $��	210,0,0,0q�6R   $
 Wrap=�6e   $��Center=�6y   $	��147��6�   $��22��6�   52      � 53      $��
376,0,-1,1q�6     54      �� 55      $��0* �6$   �� 56      .+�58      �� -	   5         ��Name$ BotonAgregarProveedor��58      $��Agregar��68   $
��Left=�6J   $��	29,28,0,0q�6e   $��2��6x   $	��Top=�6�   $	��148��6�   $��Arial Black)�6�   $��32��6�   
���   �6�   �� 59      -
   5        $ BotonEliminarProveedor��59      $��Eliminar��69   $
��Left=�6L   $��	29,80,0,0q�6g   $��2��6z   $	��Top=�6�   $	��148��6�   
���   �$��32��6�   $��Arial Black)�6�   6�   �� 5:      -   5        $ BotonEditarProveedor��5:      $��Editar��67   $
��Left=�6H   $��29,0,0,0q�6c   $��Center=�6u   $	��148��6�   
���   �$��31��6�   $��Arial Black)�6�   $��1��6�   6�   �� 5;      -   5        $! BotonImportarDatosProveedor��5;      $��Importar Datos��6>   $
��Left=�6W   $��
29,190,0,0q�6r   $��2��6�   $	��Top=�6�   $	��148��6�   
���   �$��32��6�   $��Arial Black)�6�   6�   5<      5=      ��5?   
   �� 5@       �� EndPoint$	 1,1&�6"    �� 
StartPoint$	 0,0&�61   .��5A      �  � Color$ 	#FFFBABAD��6    � Offset$ 0Q�61   � 5B      $ 	#FFFDCECF��6   $ 1Q�61   5C      5I      5J      