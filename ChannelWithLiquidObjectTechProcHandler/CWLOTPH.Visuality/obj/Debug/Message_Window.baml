   M S B A M L   `   `   `  ����  CWLOTPH.Visuality%    CWLOTPH.Visuality.MessageWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Admin_USER_Add_Window��5   	   $��229.447��6'   $��418.085��68    � WindowStartupLocation$ CenterScreen=�6H    � AllowsTransparency .  � WindowStyle$
 None=�6�   ��5      �� 5   
    �� ImageSource$  Resources/littleWindow.png��6    � Icon 5      �� 5	   
    �� 	UriSource$ Resources/icon.png?�6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ 
exitButton��5      $�� ��6%   $
��Left=�60   $��357,0,0,184q�6K   $��Bottom=�6`   $��38��6{   	 J� RenderTransformOrigin$	 -0.543,0.15&�6�   $��35��6�   $��2��6�   ��5      �� 5      
 �� 
TargetType"
 ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      $" Resources/exitNotClicked.png��6)   5      �� 5       � �� 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� #��z��6)   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   5      5!       �� Triggers 5"      P� 5#       P� Property �@J�IsMouseOver P� Value . . �5$      ��  � �� 5%   "   �� 5&   &   $ Resources/exitClicked.png��61   5(      5)      5+      5-      �� 5.   
   -   5        $ okButton��5.      $�� ��6#   $	 0.577,8&�6.   $��110,-647,117,823q�6N    J� Opacity$	 0.9Q�6�    �� 	ClickMode$ Press=�6�   $��0q�6�   $��2��6�   ��6�   Z� ��6�   Z� z�5/      V� 50      .~�51      ��  �� ScaleY$ -1Q�6%   �� 52      �� 53      T� 54      55      ��57      �� 58      "
 ��6   .�59      ��  � �� 5:      �� 5;      $  Resources/okNotClicked.png��6)   5=      �� 5>       � �� 5?      �� 5@      " ��6.   .R�5A   "   �� #��z��6)   ._�5B   &   �� $��Center=�67   $��Center=�6T   5C   #   5D      5F       5G      P� 5H       �@J�IsMouseOver . . �5I      ��  � �� 5J   "   �� 5K   &   $ Resources/okClicked.png��61   5M      5N      5P      5R      �� 5S   
   -   5         ��Name$ 	textLabel��5S      $�� ��6#   $
��Left=�6.   $��	38,29,0,0q�6I   $	��Top=�6\   $	��335��6t   $	��103��6�   $
��Bold"�6�   $��	Open Sans)�6�   $��14(�6�   $��2��6�   5T      5U      