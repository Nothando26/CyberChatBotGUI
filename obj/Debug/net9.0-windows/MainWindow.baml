   M S B A M L   `   `   `  ����  CyberChatBotPOEGUI%    CyberChatBotPOEPart1.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Cyber ChatBot��5   	   $	��650��5   	   $
��1100��6    � WindowStartupLocation$ CenterScreen=�5   	   
�������.��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ GreetingTextBlock��5      $��22(�5      $
��Bold"�5      
����i��$��Center=�5      $
��0,10q�5      �� 5   
   $��1��6   $
��Auto=�6$   $	��0,5q�6G   .��5      �� -   5        $ OutputTextBlock��5      $
��Wrap=�5      $��16(�5      
������
������� �� Padding$ 10q�5       5!      � 5$   
   $��2��6   $��0,10,0,0q�6   
�������$��40��6E    � ColumnDefinitions 5%      �� 5&      $��* �6#   �� 5(      $	��460 �6#   �� 5*      $	��110 �6#   .+�5.      �� -   5         ��Name$ UserInputBox��5.      $��1��5/      $	��460��50      $��35��51      $��16(�52      $��5q�53      ���#E91E63$��2q�55      
�������$��Center=�57      �� 59      -   5         ��Name$ SubmitButton��59      $��2��5:      $
��Send��5;      $	��100��5<      $��35��5=      $��10,0,0,0q�5>      ���#F06292
�������$
��Bold"�5A      $��0q�5B      5C      5D      5E      5F      