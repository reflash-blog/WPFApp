   M S B A M L   `   `   `  ����  CWLOTPH.Visuality.Logon)    #CWLOTPH.Visuality.Logon.LogonWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  	LogonForm��5   	   $��241.045��6   $��395.149��6,    � WindowStyle$
 None=�6<    � AllowsTransparency .  � 
ResizeMode$ NoResize=�6i    � WindowStartupLocation$ CenterScreen=�6   #��� ��6�   ��5      �� 5   
    �� ImageSource$ Resources/loginWindow.png��6    � Icon 5	      �� 5
   
    �� 	UriSource$ Resources/icon.png?�6   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��372* �6   �� 5      $	��23* �6   	 � RowDefinitions	 5   
   �� 5      $��0* �6   �� 5      $��69 �6   �� 5      $��63 �6   �� 5      $��36 �6   �� 5      .+�5   
   �� -   5        
 ��Name$
 
exitButton��5      $�� ��6%   $
��Left=�60   $��52��6K   $��
343,0,0,25q�6V    J� RenderTransformOrigin$ 	0.038,1.9&�6j    J� Opacity$	 0.9Q�6�   $��2��6�   $��2��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      $" Resources/exitNotClicked.png��6)   5      �� 5        � �� 5!      �� 5"       �� 
TargetType" ��6.   .R�5#   "   �� #��z��6)   ._�5$   &   �� $��Center=�67   $��Center=�6T   5%   #   5&      5(       �� Triggers 5)      P� 5*       P� Property �@J�IsMouseOver P� Value . . �5+      ��  � �� 5,   "   �� 5-   &   $ Resources/exitClicked.png��61   5/      50      52      54      �� 55   
   -   5        $
 okButton��55      $�� ��6#   $
��Left=�6.   $	��162��6I   $ 0.577,8&�6U   $��
198,0,0,29q�6u   $��4��6�   $	 0.9Q�6�    �� 	ClickMode$ Press=�6�   $��0q�6�   ��6�   Z� ��6�   Z� ��56      �� 57      " ��6   .�58      ��  � �� 59      �� 5:      $  Resources/okNotClicked.png��6)   5<      �� 5=       � �� 5>      �� 5?      " ��6.   .R�5@   "   �� #��z��6)   ._�5A   &   �� $��Center=�67   $��Center=�6T   5B   #   5C      5E       5F      P� 5G       �@J�IsMouseOver . . �5H      ��  � �� 5I   "   �� 5J   &   $ Resources/okClicked.png��61   5L      5M      5O      5Q      �� 5R   
   -   5         ��Name$ loginTextBox��5R      $
��Left=�6(   $��20��6C   $��	198,0,0,0q�6O   $��2��6b    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��174��6�   $	 0.9Q�6�   G� 5S   
   -   5         G�Name$ passwordTextBox��5S      $
��Left=�6/   $��	198,0,0,0q�6J   $��3��6]   $	��Top=�6j   $	��174��6�   $��20��6�   $	 0.9Q�6�   5T      5V      