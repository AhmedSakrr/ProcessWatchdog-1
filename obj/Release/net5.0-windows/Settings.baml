   M S B A M L   `   `   `  ����   
PBWatchdog    PBWatchdog.Settings   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     @d2http://schemas.microsoft.com/expression/blend/2008     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Einstellungen�� � 
ResizeMode$ NoResize=�$	��190��$	��480�� � WindowStyle$
 None=� � ShowInTaskbar$
 True��$y�Visible=� � WindowStartupLocation$ CenterScreen=�'  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 0q�   CornerRadius$ 0��   CaptionHeight$ 0Q�.���� $��1q�
���   �._��� ���#424242.+�� ���#c62828$��25��	 � ColumnDefinitions	 �� $��* ��� $
��auto �.+��� -   $��0��$��13(����#e0e0e0$&�� Benutzerdefinierte Einstellungen��$
��Bold"�$��
5, 0, 0, 0q�$��Center=��� -   
 ��Name$
 btnExit��$��1��$
��25px��$��Stretch=�$��12(�$
��Bold"�$��0q�
������ $��X���� $��1q�
���   �$��1��� $�� 0, 10q�	 �� $��0.02* ��� $
��auto ��� $��* �.+��� $��1q�
�������$��1��$��2 2 2 0q��� $��Optionen��
�������$��13(�$��1���� $��1q�
�������$��1��$��2��$��2 2 10 0q�� 	 �� $
��auto ��� $
��auto ��� $
��auto �.+��� -    ��Name$ cBoxAutostart��$��	Autostart�� � ToolTip$. (Schreibt den PBWatchdog in den Autostart��
�������$
��5, 0q�$��0���� -   $ cBoxMonitor��$#��Monitor beim Start ausblenden��$j dDer Monitor wird beim Start nicht angezeigt. Tray Icon und Benachrichtigungen funktionieren trotzdem��
�������$
��5, 0q�$��1���� -   $ cBoxShowNameInMonitor��$��Name im Monitor anzeigen��$O IDer Benutzername wird im Monitor angezeigt wenn die Postbox geöffnet ist��
�������$
��5, 0q�$��2��� $�� 0, 10q�	 �� $��0.02* ��� $
��auto ��� $��* �.+��� $��1q�
�������$��1��$��2 2 2 0q��� $��Benachrichtigungen��
�������$��13(�$��1���� $��1q�
�������$��1��$��2��$��2 2 10 0q�� 	 �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��Benachrichtigungs Zeiten��
�������$
��5, 0q�$��0���� -    ��Name$ txbTimeStart��$��37��$��1��$�� �� �� 	MaxLength$ 5���� $	��bis��
�������$
��5, 0q�$��2���� -   $ txbTimeStop��$�� ��$��37��$��3��$ 5���� $	��Uhr��
�������$
��5, 0q�$��4���� -   $ cBoxWeekendNotification��$��
Wochenende��$< 6Benachrichtigungen werden auch am Wochenende angezeigt��
�������$��7 0 0 0q�$��5��� .+��� -	   $
 btnSave��$��	Speichern��$��80��$��30q�
���   �$��0q�