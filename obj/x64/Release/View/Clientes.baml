   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  (    "Comercial_Estefannny.View.Clientes   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .��� .+��   � RowDefinitions  �� $	��51* ��� $	��50* � � ColumnDefinitions �� $��* �.+�� $��1�� �� .+��� -    ��Name$ ClientesListView��$��10,23,10,10q�$��Visible=�$��Arial Black)�$��11(�$��Visible=� �� View �� .P��� $��Nombre�� �� Width$	 300�� �� DisplayMemberBinding �� *Nombre+�� $��Telefono��$	 150�� �� *	Direccion+�� $��	Direccion��$	 150�� �� *
Telefono+�� $��Deuda��$	 100�� �� *Deuda+�� -    ��Name$ TextBuscadorCliente��$
��Left=�$��10,0,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��171��$��18��� $��	0,1,375,0q�  �� �� �� �� .+��� -   $ NombreCliente��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��NOMBRE:��$��Center=�$��92��$��Arial Black)�$��16(� J� 	AllowDrop . 	 �� 
IsReadOnly	 . $��20����Z� ��Z� 
 �� SelectionBrush
 Z� �� -   $ DireccionCliente��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��
DIRECCION:��$��Center=�$	��118��$��Arial Black)�$��16(� . 	 . $��2��$��20����Z� ��Z� 
 Z� �� -   $ TelefonoCliente��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	TELEFONO:��$��Center=�$	��118��$��Arial Black)�$��16(� . 	 . $��1��$��20����Z� ��Z� 
 Z� �� -   $ DeudaCliente��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��DEUDA:��$��Center=�$	��152��$��Arial Black)�$��16(� . 	 . $��3��$��20����Z� ��Z� 
 Z� �� -   $ TextDeudaCliente��$
��Left=�$��	210,0,0,0q�$��3��$
 Wrap=�$��Center=�$	��147��$��22���� -   $ TextDireccionCliente��$
��Left=�$��	210,0,0,0q�$��2��$
 Wrap=�$��Center=�$	��147��$��22���� -	   $ TextTelefonoCliente��$
��Left=�$��	210,0,0,0q�$��1��$
 Wrap=�$��Center=�$	��147��$��22���� -
   $ TextNombreCliente��$
��Left=�$��	210,0,0,0q�$
 Wrap=�$��Center=�$	��147��$��22��� $��
376,0,-1,1q�  �� $��0* ��� .+��� -    ��Name$ BotonAgregarCliente��$��Agregar��$
��Left=�$��	29,28,0,0q�$��2��$	��Top=�$	��148��$��Arial Black)�$��32��
���   ��� -   $ BotonEliminarCliente��$��Eliminar��$
��Left=�$��	29,80,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)��� -   $ BotonEditarCliente��$��Editar��$
��Left=�$��29,0,0,0q�$��Center=�$	��148��
���   �$��31��$��Arial Black)�$��1���� -   $ BotonImportarDatosCliente��$��Importar Datos��$
��Left=�$��
29,196,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)�����  �� EndPoint$	 1,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFFBABAD�� � Offset$ 0Q�� $ 	#FFFDCECF��$ 1Q�