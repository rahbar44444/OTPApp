   M S B A M L   `   `   `  ����A  =OTPApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:OTPAppOTPApp  5   	       OTPApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:OTPApp     5   	     � Title$  
MainWindow��5   	   $	��650��6   $	��800��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value �� CornerRadius � ToolTip �� ElementName �� Path��%  '��       '�� t     �� 5
   
   " ��6   .�5      ��  � ��$ 5,0,5,0q�6'   �� 5       � ��$ Right=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 0,2,40,2q�6'   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      ]� $
��true��6$   .+�5      �� 
���  ��$��right=�66   $��5,0,0,0q�6M   $��20��6^   $��20��6i   $ 10��6u    5   %   �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5   "   �� $��!��6,   $��center=�65   $��center=�6P   $
��Bold"�6m   
�������5   #   5      �� 5      $��customAdorner��68   $��Center=�6M   .v�5   "   �� 
���  ��$��1q�6;   5      5      5      5!      5"      5#      .��5$      � 	 � ColumnDefinitions	 5%   
   �� 5&      $	��100 �6   �� 5'      $��* �6#   �� 5(      $	��100 �6#   .+�5*   
   � $��1��6   
 � RowDefinitions
 5+      �� 5,      $	��100 �6    �� 5-      $��* �6    �� 5.      $	��100 �6    .+�50      � $��1��6   
 51      �� 52      $	��100 �6$   �� 53      $	��100 �6$   �� 54      $	��100 �6$   �� 55      $	��100 �6$   	 57      �� 58      $��* �6'   �� 59      $��3* �6'   .+�5;      �� $��Email��6   $��30(�6)   $��DemiBold"�67   $��Center=�6M   $��Center=�6j   �� 5<      -   5         ��Name$ txtEmail��5<      $��1��5=      $��30(�5@      $��Stretch=�5A      $��50��5B      $��0 0 0 0q�5C      ��5>      �� *Email+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��5C      �� 5D      -   5        $��Send OTP��5D      $	��175��6,   $��Right=�5E      $��1��5F      $��1��6)   $��
20 20 0 20q�5I      $��30(�5J      $��DemiBold"�5K      {�5H      �� *IsSendOTPEnable+$ TwoWay=�$ PropertyChanged=�5K      �� 5M      $��	Enter OTP��6   $��2��6-   $��30(�6:   $��DemiBold"�6H   $��Center=�6^   $��Center=�6{   �� 5N      -   5        $ txtOTP��5N      $��2��5O      $��1��5P       �� 	MaxLength$ 6��5Q      $��30(�5R      $��Stretch=�5X      $��50��5Y      $��0 0 0 0q�5Z      ��5S      �� *OTP+$ TwoWay=�$ PropertyChanged=�$
 True��5Z      �� 5[      -   5        $��
Verify OTP��5[      $	��175��6.   $��Right=�5\      $��1��5]      $��3��5^      $��
20 20 0 20q�5a      $��30(�5b      $��DemiBold"�5c      {�5_      �� *IsVerifyOTPEnable+$ TwoWay=�$ PropertyChanged=�5c      5d      5e      5f      5g      