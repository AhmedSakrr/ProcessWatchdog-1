   M S B A M L   `   `   `  ����E  APBWatchdog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PBWatchdog
PBWatchdog  5   	       PBWatchdog.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DNotifyIconWpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5   	   $localclr-namespace:PBWatchdog  5   	     � Title$  PB Watchdog��5	   	   $	��112��5
   	   $	��100��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � ShowInTaskbar$ False��5   	   $y�Visible=�5   	    � Topmost$
 True��5   	   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ 0q�5         CornerRadius$ 0��5         CaptionHeight$ 0Q�5      .��5      �� 
���   �$��1q�6!   ._�5   
   �� .+�5      � ���#9e9e9e	 � ColumnDefinitions	 5      �� 5      $��* �6'   �� 5       $
��auto �6'   .+�5"      �� -   5        $��0��5#      $��11(�5%      
���   �$��
PBWatchdog��5'      $��
5, 2, 0, 0q�5(      �� 5*      -   5        
 ��Name$
 btnExit��5+      $��1��5,      $
��25px��5-      $��20��5.      $	��Top=�50      $��12(�51      $
��Bold"�52      $��0q�53      
������ $��X��55      57      �� 58      $��1q�59      
���   �$��1��5;      � 5=      	 5>      �� 5?      $��* �6'   .+�5A      �� -   5        $
 btn_PB��5B         MyDesign#���   5A      $
��PBIZ��5C      $��20(�5D      
�������$	��100��5F      $��90��5G      $��0q�5H      $��Center=�5I      $��Bottom=�5J      �� 5L      -   5         ��Name$ txbUserName��5M       �� TextAlignment$ Center=�5N      $�� ��5O      $��0,65,0,1q�5P      $��11(�5Q      
�������5R      5T      � 5V      .+�5W      3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$ tb��5W   !      ToolTipText$ 
PBWatchdog��6-   $y�Visible=�6l    � ContextMenu 5X      �� 5Y      .@�5Z      m� -   5         m�Name$ TrayShowMain��5Z   '   $��Monitor anzeigen��6=   6W   m� 5[      -   5        $ TrayExportLog��5[   '   $��Log exportieren��6>   6W   m� 5\      -   5        $ TraySettings��5\   '   $��Einstellungen��6=   6T   m� 5]      -	   5        $ TrayExit��5]   '   $��Beenden��69   6J   5^      5`      5a      5b      5c      5d      