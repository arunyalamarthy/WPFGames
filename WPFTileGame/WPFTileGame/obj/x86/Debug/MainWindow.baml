   M S B A M L   `   `   `  ����F  BWPFTileGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFTileGameWPFTileGame  5   	       WPFTileGame.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   )localclr-namespace:WPFTileGame    5   	   $	��525��5   	   $	��525��6     � 	Resources  5          myBrush �� Color  WPFTileGame.GameButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� RadiusX �� RadiusY	 J� Opacity
 � Color � Offset �� Triggers P� Property P� Value O� 	GlowColor O� GlowSize �� 
TargetName :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration��%"  &	        '       �� 5   
   $ Black��6+   �� 5   
   "  6   .�5	      ��  � �� 5
      �� 5      "  6&   .R�5      � .+�5   "   �� $ 
mainButton��6,   
�������
�������$��5��6_   $ 5��6s   $ 5��6   5   #   �� 5   "   $��2q�63   $��Center=�6>   $��Center=�6[   �� 5   "   $ buttonHoverButton��6,   $	 0Q�6G   $ 5��6S   $ 5��6_   ��5   &   
� 5   *   .��5   .   � $
 White��6;   $ 1Q�6I   � 5   .   $
 Indigo��6;   $	 0.9Q�6J   � 5   .   $

 Blue��6;   $	 0.8Q�6H   � 5   .   $
 	LawnGreen��6;   $	 0.7Q�6M   � 5   .   $
 Yellow��6;   $	 0.6Q�6J   � 5   .   $
 Orange��6;   $	 0.5Q�6J   � 5   .   $
 Violet��6;   $	 0.4Q�6J   � 5   .   $
 Transparent��6;   $ 0Q�6O   5   +   5   #   5        5!      P� 5"       �@J�IsMouseOver . . �5#   "   ��  �@J�BitmapEffect 5$   &   O� 5%   *   $ Violet��6@   $ 10Q�6S   5'   #   5(      P� 5)       � �� . . �5*   "   �� $ 
mainButton��6)    � ��
 � ���6Q   5+      :� 5,      $ Button.MouseEnter��6+   [�5-   "   �� 5.   &   .��5/   *   �� .��50   .   W� $ buttonHoverButton��6>   $ Opacity�6h   $ 1X�6�   $ 0:0:0.25D�51   -   52   +   53   '   55      :� 56      $ Button.MouseLeave��6+   [�57   "   �� 58   &   .��59   *   �� .��5:   .   W� $ buttonHoverButton��6>   $ Opacity�5;   -   $ 0X�6Q   $ 0:0:0.5D�6X   5<   +   5=   '   5?      5A      5C      5D      5E      5F      