   M S B A M L   `   `   `  ����  CWLOTPH.Visuality0    *CWLOTPH.Visuality.Admin_USER_Update_Window   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Admin_USER_Update_Window��5   	   $��287.958��6*   $��425.532��6;    � WindowStartupLocation$ CenterScreen=�6K    � AllowsTransparency .  � WindowStyle$
 None=�6�   ��5      �� 5   
    �� ImageSource$  Resources/littleWindow.png��6    � Icon 5      �� 5	   
    �� 	UriSource$ Resources/icon.png?�6   .��5      � .+�5   
   �� -   5         ��Name$ 
exitButton��5      $�� ��6%   $
��Left=�60   $��368,0,0,243q�6K   $��Bottom=�6`   $��38��6{    J� RenderTransformOrigin$ -0.543,0.15&�6�   $��35��6�   ��5      �� 5      	 �� 
TargetType"	 ��6   .�5      �� 
 �� Property
 � �� �� Value 5      �� 5      $" Resources/exitNotClicked.png��6)   5      �� 5      
 � �� 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� #��z��6)   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      �� 
 � �� 5    "   �� 5!   &   $ Resources/exitClicked.png��61   5#      5$      5&      5(      �� 5)   
   -   5        $ updateButton��5)      $�� ��6'   $
��Left=�62   $	��162��6M   $ 0.577,8&�6Y   $��244,195,0,47q�6y    J� Opacity$	 0.9Q�6�    �� 	ClickMode$ Press=�6�   $��0q�6�   ��6�   Z� ��6�   Z� ��5*      �� 5+      "	 ��6   .�5,      �� 
 � �� 5-      �� 5.      $$ Resources/updateNotClicked.png��6)   50      �� 51      
 � �� 52      �� 53      " ��6.   .R�54   "   �� #��z��6)   ._�55   &   �� $��Center=�67   $��Center=�6T   56   #   57      59       5:      P� 5;       �@J�IsMouseOver . . �5<      �� 
 � �� 5=   "   �� 5>   &   $! Resources/updateClicked.png��61   5@      5A      5C      5E      �� 5F   
   -   5         ��Name$ idLabel��5F      $��ID��6!   $
��Left=�6.   $��	42,55,0,0q�6I   $	��Top=�6\   $��71��6t   $��28��6   $��	Open Sans)�6�   $��14(�6�   $
��Bold"�6�   �� 5G   
   -   5         ��Name$ 	idTextBox��5G      $
��Left=�6%   $��29��6@   $��
147,55,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��120��6�   {�.  6�   �� 5H   
   -   5        $ 
loginLabel��5H      $��Login��6$   $
��Left=�64   $��	42,89,0,0q�6O   $	��Top=�6b   $��71��6z   $��37��6�   $
��Bold"�6�   $��	Open Sans)�6�   $��14(�6�   �� 5I   
   -   5        $ loginTextBox��5I      $
��Left=�6(   $��29��6C   $��
147,89,0,0q�6O   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   {�.  6�   �� 5J   
   -   5        $ passwordLabel��5J      $��Password��6'   $
��Left=�6:   $��
42,123,0,0q�6U   $	��Top=�6i   $��85��6�   $��37��6�   $
��Bold"�6�   $��	Open Sans)�6�   $��14(�6�   �� 5K   
   -   5        $ passwordTextBox��5K      $
��Left=�6+   $��29��6F   $��147,123,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��120��6�   5L      5M      