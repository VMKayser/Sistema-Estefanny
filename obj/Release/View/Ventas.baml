   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  *    $Comercial_Estefannny.View.VentasView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     � 	Resources     DatePickerCalendarStyle1 �� 
TargetType&   System.Windows.Controls.Calendar �� Property �� Value �� EndPoint �� 
StartPoint � Color � Offset �� 
TargetType	 ��Name5  /System.Windows.Controls.Primitives.CalendarItem
  Name   CalendarItemStyle��%  &	        �� "  .���  � �� �	#FF333333��  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE4EAF0��$ 0Q�� $ 	#FFECF0F4��$
 0.16Q�� $ 	#FFFCFCFD��$
 0.16Q�� $ 	#FFFFFFFF��$ 1Q���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA3AEB9��$ 0Q�� $ 	#FF8399A9��$ 0.375Q�� $ 	#FF718597��$ 0.375Q�� $ 	#FF617584��$ 1Q���  � ��$ 1q���  � �� �� "  .R��� $	 	PART_Root��$��Center=�.+�  $
 PART_CalendarItem��#��z��#��z��#��z��#��z .���  J� RenderTransformOrigin$ 0.512,0.822&� � RowDefinitions �� $	��87* ��� $	��40* � � ColumnDefinitions �� $
��638* ��� $��1003* �.+�� $��0��$	��606��$��Center=�$��0,0,4,0q� �� $	��47* ��� $	��47* ��� $	��47* ��� $	��47* ��� $	��47* ��� $	��47* ��� $	��47* ��� $	��62* �.+�� $��8��$��0��$��-10,10,10,0q� �� $	��62* ��� $	��63* �.+�(  "System.Windows.Controls.DatePicker  -     Name$ 
FechaVenta��   CalendarStyle# �   $��1��$
��Left=�$��6,10,0,0q����	#FF333333$��Arial Black)�$��16(� � Cursor$ AppStarting��$	��250��$��39��$	��Top=���Z� ��Z�    SelectedDate �� *Fecha+ �� Mode$ TwoWay=��� -    ��Name$ ComboProducto��$��1��$��Center=�$��	0,225,0,0q�$	��Top=�$	��300��$��38�� �� 
IsEditable .  �� IsTextSearchEnabled .   �� DisplayMemberPath$ NombreProducto��$��Arial Black)�$��16(����� *
Productos1+�� -   $ 
ComboMarca��$��1��$
��Left=�$��	8,151,0,0q�$	��Top=�$	��302��$��40�� .  . $��Arial Black)�$��16(����� *Marcas+�� -   $ ComboCategoria��$��1��$
��Left=�$��8,78,0,0q�$	��Top=�$	��302��$��35�� .  . $��Arial Black)�$��16(����� *
Categorias+�� -   $ ComboVariante��$��1��$��Center=�$��	0,298,0,0q�$	��Top=�$	��300��$��38�� .  . $��Arial Black)�$��16(����� *	Variantes+%  Xceed.Wpf.Toolkit.DecimalUpDown  -     Name$ DecimalCantidad��$��1��$
��Left=�$��
28,365,0,0q�$	��Top=�$	��101��$��31��$��Arial Black)�$��16(��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 1X�   Minimum$ 1X�  -   $ DecimalPrecio��$��1��$
��Left=�$��
28,442,0,0q�$	��Top=�$	��101��$��35��$��Arial Black)�$��16(� �� *PrecioVenta+$ TwoWay=�  -	   $ DecimalDescuento��$��1��$
��Left=�$��
28,531,0,0q�$	��Top=�$	��101��$��32��$��Arial Black)�$��16(�$ 0X�$ 0X��� -
    ��Name$ 	TextFecha��$
��Left=�$��10,0,0,0q� �� TextWrapping$
 Wrap=�$��FECHA��$��Center=�$	��123��$��Arial Black)�$��20(���Z� ��Z�  �� SelectionBrush Z� �� -   $ TextCategoria��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	CATEGORIA��$��Center=�$	��152��$��Arial Black)�$��20(�$��1����Z� ��Z�  Z� �� -   $ 	TextMarca��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��MARCA��$��Center=�$	��152��$��Arial Black)�$��20(�$��2����Z� ��Z�  Z� �� -   $ TextoProducto��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��PRODUCTO��$��Center=�$	��152��$��Arial Black)�$��20(�$��3����Z� ��Z�  Z� �� -   $ TextoVariante��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��VARIANTE��$��Center=�$	��163��$��Arial Black)�$��20(�$��4����Z� ��Z�  Z� �� -   $ TextoCantidad��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��CANTIDAD��$��Center=�$	��137��$��Arial Black)�$��20(�$��5����Z� ��Z�  Z� �� -   $ Texto_Precio��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��PRECIO��$��Center=�$	��163��$��Arial Black)�$��20(�$��6����Z� ��Z�  Z� �� -   $ TextoDescuento��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	DESCUENTO��$��Center=�$	��152��$��Arial Black)�$��20(�$��7����Z� ��Z�  Z� �� -    ��Name$ ListaProductosVenta��$��	15,48,0,6q�$��Arial Black)�
�������$��Disabled=�$��Visible=�$��Stretch=�$��Center=�$��1��$��16(���Z� .@��� -   $ ProductosListView������ *ProductosDeVenta+ �� SelectedItem �� *productoSeleccionado+$ TwoWay=�  �� View  �� .P��� $��Nombre��! �� Width$	! 300��" �� DisplayMemberBinding" �� *NombreProducto+�� $��Variante��$	! 130��" �� *
Variante+�� $��Cantidad��$	! 130��" �� *
Cantidad+�� $��Precio��$	! 130��" �� *Precio+�� $��	Descuento��$	! 130��" �� *	Descuento+�� $��Total��$	! 130��" �� *Total+�� -   $ 	TextLista��$��326,10,416,0q�$
 Wrap=�$��LISTA DE PRODUCTOS��$	��Top=�$��Arial Black)�$��20(�$��1��$��20����Z� ��Z�  Z� �� -   $ ComboCliente��$��1��$��335,160,427,118q� .  . $��1��$ Nombre��$ 0.512,0.614&�$��20(�$��Arial Black)�$��Center=�$��Center=����� *clientesCache+�� -   $ ComboTipoDePago��$��1��$��335,93,427,184q� .  # �� SelectedIndex$# 1��$��1��$��20(�$��Arial Black)�$��Center=�$��Center=�.@��� .���� $��QR��$ �� TextAlignment$$ Center=�$��Center=��� .���� $��Efectivo��$$ Center=�$��Center=��� .���� $��Crédito��$$ Center=�$��Center=��� -   $ TextTipoDePago��$��27,98,794,0q�$
 Wrap=�$��TIPO DE PAGO��$	��Top=�$��Arial Black)�$��1��$��1��$��40��$��20(���Z� ��Z�  Z� �� -   $ TextCliente��$��27,160,853,0q�$
 Wrap=�$��CLIENTE��$	��Top=�$��Arial Black)�$��1��$��1��$��32��$��20(���Z� ��Z�  Z� �� -   $ TextTotalPagar��$��	27,25,0,0q�$
 Wrap=�$��TOTAL A PAGAR��$	��Top=�$��Arial Black)�$��1��$��1��$��32��$��20(�% �� TextAlignment$% Center=�$
��Left=���Z� ��Z�  Z� �� -   $ TextTotalApagar��$��1��$
��Left=�$��
335,25,0,0q�$��1��$
 Wrap=�$	��Top=�$	��241��$��41��$��Arial Black)�$��20(�$% Center=����� *TotalAPagar+$ OneWay=�& �� StringFormat$& 
Bs. {0:N2}���� -   ' ��Name$' BotonEditar��$��Editar Producto��$
��Left=�$��	10,20,0,0q�$	��Top=�$	��201��$��Arial Black)�$��46��
���   �$��1��$��20(��� -   $' BotonEliminar��$��Eliminar Producto��$
��Left=�$��
257,20,0,0q�$	��Top=�$	��223��$��Arial Black)�$��46��
���   �$��1��$��20(��� -   $' BotonAgregar��$��Agregar Producto��$
��Left=�$��	10,95,0,0q�$	��Top=�$	��201��$��Arial Black)�$��45��
���   �$��1��$��20(��� -   $' BotonRegistrar��$��Registrar Venta��$
��Left=�$��
257,95,0,0q�$	��Top=�$	��223��$��Arial Black)�$��45��
���   �$��1��$��20(����� $	 1,1&�$	 0,0&�.��� $ 	#FFFBABAD��$ 0Q�� $ 	#FFFDCECF��$ 1Q�