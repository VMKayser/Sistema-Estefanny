   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  '    !Comercial_Estefannny.View.Compras   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � 	Resources     DatePickerCalendarStyle1 �� 
TargetType&   System.Windows.Controls.Calendar �� Property �� Value �� EndPoint �� 
StartPoint � Color � Offset �� 
TargetType	 ��Name5  /System.Windows.Controls.Primitives.CalendarItem
  Name   CalendarItemStyle��%  &	        �� "  .���  � �� �	#FF333333��  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4EAF0��$ 0Q�� $ 	#FFECF0F4��$
 0.16Q�� $ 	#FFFCFCFD��$
 0.16Q�� $ 	#FFFFFFFF��$ 1Q���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA3AEB9��$ 0Q�� $ 	#FF8399A9��$ 0.375Q�� $ 	#FF718597��$ 0.375Q�� $ 	#FF617584��$ 1Q���  � ��$ 1q���  � �� �� "  .R��� $	 	PART_Root��$��Center=�.+�  $
 PART_CalendarItem��#��z��#��z��#��z��#��z .���  � RowDefinitions �� $
��188* ��� $
��115* � � ColumnDefinitions �� $
��268* ��� $
��483* �.+�� $��0��$	��606��$	��Top=�$��2�� �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��5* �.+�� $��0��$��8��$��0�� �� $	��62* ��� $	��63* �.+�(  "System.Windows.Controls.DatePicker  -     Name$ FechaCompra��   CalendarStyle# �   $��1��$
��Left=�$��6,10,0,0q����	#FF333333$��Arial Black)� � Cursor$ AppStarting��$	��136��$��24��$	��Top=���Z� ��Z�    SelectedDate �� *Fecha+ �� Mode$ TwoWay=��� -    ��Name$ ComboProducto��$��1��$��Center=�$��	0,149,0,0q�$	��Top=�$	��121��$��21�� �� 
IsEditable .  �� IsTextSearchEnabled .   �� DisplayMemberPath$ NombreProducto��$��Arial Black)�$��12(� J� RenderTransformOrigin$ 0.5,0.5&����� *
Productos1+z�V� .~��� ��  �� AngleX$ 2.114Q��� T�  T� X$ -0.554Q��� -   $ 
ComboMarca��$��1��$
��Left=�$��	6,105,0,0q�$	��Top=�$	��120��$��22�� .  . ���� *Marcas+�� -   $ ComboCategoria��$��1��$
��Left=�$��6,56,0,0q�$	��Top=�$	��120��$��22�� .  . ���� *
Categorias+�� -   $ ComboVariante��$��1��$
��Left=�$��	6,198,0,0q�$	��Top=�$	��120��$��22�� .  . ���� *	Variantes+%  Xceed.Wpf.Toolkit.DecimalUpDown  -     Name$ DecimalCantidad��$��1��$
��Left=�$��	6,245,0,0q�$	��Top=�$��76��$��20���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� *
Cantidad+$ TwoWay=�  -   $ DecimalPrecio��$��1��$
��Left=�$��	6,291,0,0q�$	��Top=�$��76��$��20�� �� *PrecioVenta+$ TwoWay=�  -   $ DecimalDescuento��$��1��$
��Left=�$��	6,336,0,0q�$	��Top=�$��76��$��20�� �� *	Descuento+$ TwoWay=��� -	    ��Name$ 	TextFecha��$
��Left=�$��10,0,0,0q� �� TextWrapping$
 Wrap=�$��FECHA��$��Center=�$��69��$��Arial Black)���Z� ��Z�  �� SelectionBrush Z� �� -
   $ TextCategoria��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	CATEGORIA��$��Center=�$	��112��$��Arial Black)�$��1����Z� ��Z�  Z� �� -   $ 	TextMarca��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��MARCA��$��Center=�$	��152��$��Arial Black)�$��2����Z� ��Z�  Z� �� -   $ TextoProducto��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��PRODUCTO��$��Center=�$	��112��$��Arial Black)�$��3����Z� ��Z�  Z� �� -   $ TextoVariante��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��VARIANTE��$��Center=�$	��101��$��Arial Black)�$��4����Z� ��Z�  Z� �� -   $ TextoCantidad��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��CANTIDAD��$��Center=�$	��101��$��Arial Black)�$��5����Z� ��Z�  Z� �� -   $ Texto_Precio��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��PRECIO��$��Center=�$��86��$��Arial Black)�$��6����Z� ��Z�  Z� �� -   $ TextoDescuento��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	DESCUENTO��$��Center=�$	��123��$��Arial Black)�$��7����Z� ��Z�  Z� �� -    ��Name$ BotonAgregar��$��Agregar Producto��$
��Left=�$��	10,84,0,0q�$	��Top=�$	��148��$��Arial Black)�$��32��
���   �$��8���� -   $ BotonEliminar��$��Eliminar Producto��$
��Left=�$��	10,47,0,0q�$	��Top=�$	��148��$��Arial Black)�$��32��
���   �$��8���� -   $ BotonEditar��$��Editar Producto��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��148��$��Arial Black)�$��32��
���   �$��8���� -   $ BotonRegistrar��$��Regitrar Venta��$
��Left=�$��
10,121,0,0q�$	��Top=�$	��148��$��Arial Black)�$��32��
���   �$��8���� -     ��Name$  ListaProductosCompra��$��	0,35,0,10q�$��Arial Black)�
�������$��Disabled=�$��Visible=�$��Stretch=�$��Center=�$��1����Z� .@��� -   $  ProductosListView������ *ProductosDeCompra+! �� SelectedItem! �� *productoSeleccionado+$ TwoWay=�" �� View" �� .P��� $��Nombre��# �� Width$	# 140��$ �� DisplayMemberBinding$ �� *NombreProducto+�� $��Variante��$# 80��$ �� *
Variante+�� $��Cantidad��$# 60��$ �� *
Cantidad+�� $��Precio��$# 60��$ �� *Precio+�� $��	Descuento��$# 70��$ �� *	Descuento+�� $��Total��$# 60��$ �� *Total+�� -   $ 	TextLista��$��114,10,114,0q�$
 Wrap=�$��LISTA DE PRODUCTOS��$	��Top=�$��Arial Black)�$��16(�$��1��$��20����Z� ��Z�  Z� �� -   $ ComboProveedor��$��1��$��353,93,10,115q� .  . $��1��$ Nombre��$ 0.512,0.614&�% �� Text% �� *	Proveedor+$ TwoWay=����� *ProveedoresCache+�� -   $ TextProveedor��$��
27,0,358,0q�$
 Wrap=�$��	PROVEEDOR��$��Center=�$��Arial Black)�$��1��$��1����Z� ��Z�  Z� �� -   $ TextTotalPagar��$��27,26,336,0q�$
 Wrap=�$��TOTAL A PAGAR��$	��Top=�$��Arial Black)�$��1��$��1����Z� ��Z�  Z� �� -   $ TextTotalCompra��$��1��$
��Left=�$��
353,25,0,0q�$��1��$
 Wrap=�$	��Top=�$	��120������ *TotalAPagar+$ OneWay=�& �� StringFormat$& C������ $	 1,1&�$	 0,0&�.��� $ 	#FFFBABAD��$ 0Q�� $ 	#FFFDCECF��$ 1Q�