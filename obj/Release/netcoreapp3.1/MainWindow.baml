   M S B A M L   `   `   `  ���� J  FProcessWatchdog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PBWatchdog
PBWatchdog      PBWatchdog.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     H DNotifyIconWpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  $localclr-namespace:PBWatchdog    � Title$  
P-Watchdog��$	��112��$	��100�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � ShowInTaskbar$ False��$y�Visible=� � Topmost$
 True�� � AllowsTransparency$
 True��
������ '  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 0q�   CornerRadius$ 0��	   CaptionHeight$	 0Q�.���� 
���   �$��1q�._��� .+�� ���#9e9e9e
 � ColumnDefinitions
 �� $��* ��� $
��auto �.+��� -   $��0��$��11(�
���   �$��
P-Watchdog��$��
5, 2, 0, 0q��� -    ��Name$ btnExit��$��1��$
��25px��$��20��$	��Top=�$��12(�$
��Bold"�$��0q�
������ $��X���� $��1q�
���   �$��1��� 
 �� $��* �.+��� -   $ btn_PB��   MyDesign#���   $��Process��$��15(�
�������$	��100��$��90��$��0q�$��Center=�$��Bottom=��� -    ��Name$ txbUserName�� �� TextAlignment$ Center=�$�� ��$��0,65,0,1q�$��11(�
�������� .+�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -     Name$ tb��   ToolTipText$ 
PBWatchdog��$y�Visible=� � ContextMenu �� .@�m� -    m�Name$ TrayShowMain��$��Monitor anzeigen��m� -   $ TrayExportLog��$��Log exportieren��m� -   $ TraySettings��$��Einstellungen��m� -	   $ TrayExit��$��Beenden��