   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  +    %Comercial_Estefannny.View.Proveedores   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .��� .+��   � RowDefinitions  �� $	��51* ��� $	��50* � � ColumnDefinitions �� $��* �.+�� $��1�� �� .+��� -    ��Name$ ProveedoresListView��$��10,23,10,10q�$��Visible=�$��Arial Black)�$��11(�$��Visible=� �� View �� .P��� $��Nombre�� �� Width$	 300�� �� DisplayMemberBinding �� *Nombre+�� $��Telefono��$	 150�� �� *
Telefono+�� $��	Direccion��$	 150�� �� *	Direccion+�� -    ��Name$ TextBuscadorProveedor��$
��Left=�$��10,0,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��171��$��18��� $��	0,1,375,0q�  �� �� �� �� .+��� -   $ NombreProveedor��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��NOMBRE:��$��Center=�$��92��$��Arial Black)�$��16(� J� 	AllowDrop . 	 �� 
IsReadOnly	 . $��20����Z� ��Z� 
 �� SelectionBrush
 Z� �� -   $ DireccionProveedor��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��
DIRECCION:��$��Center=�$	��118��$��Arial Black)�$��16(� . 	 . $��2��$��20����Z� ��Z� 
 Z� �� -   $ TelefonoProveedor��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	TELEFONO:��$��Center=�$	��118��$��Arial Black)�$��16(� . 	 . $��1��$��20����Z� ��Z� 
 Z� �� -   $ TextDireccionProveedor��$
��Left=�$��	210,0,0,0q�$��2��$
 Wrap=�$��Center=�$	��147��$��22���� -   $ TextTelefonoProveedor��$
��Left=�$��	210,0,0,0q�$��1��$
 Wrap=�$��Center=�$	��147��$��22���� -   $ TextNombreProveedor��$
��Left=�$��	210,0,0,0q�$
 Wrap=�$��Center=�$	��147��$��22��� $��
376,0,-1,1q�  �� $��0* ��� .+��� -	    ��Name$ BotonAgregarProveedor��$��Agregar��$
��Left=�$��	29,28,0,0q�$��2��$	��Top=�$	��148��$��Arial Black)�$��32��
���   ��� -
   $ BotonEliminarProveedor��$��Eliminar��$
��Left=�$��	29,80,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)��� -   $ BotonEditarProveedor��$��Editar��$
��Left=�$��29,0,0,0q�$��Center=�$	��148��
���   �$��31��$��Arial Black)�$��1���� -   $! BotonImportarDatosProveedor��$��Importar Datos��$
��Left=�$��
29,190,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)�����  �� EndPoint$	 1,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFFBABAD�� � Offset$ 0Q�� $ 	#FFFDCECF��$ 1Q�