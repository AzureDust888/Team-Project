   M S B A M L   `   `   `  ����G  CTeam Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Team_ProjectTeam_Project  5   	       Team_Project.Launcher   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   &localclr-namespace:Team_Project  5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   -   5          � Title$  Launcher��5
   	   $	��600��6   $	��850��6'    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	   .��5      �� -   5         ��Name$ 
MainCanvas��5      .+�5   
   �� -   5         ��Name$ Launcher_Main_Border��5       �� CornerRadius$ 20��6/   
���   �$	��600��5      $	��850��6   5      "  FontAwesome.WPF.ImageAwesome  5   
   -   5           Icon$ Close=�5         
Foreground �#cdd9e5$��30��5      $��30��5      $��810,10q�5      5      �� 5   
   -   5        	 ��Name$	 SpawnCampFire��5!      $	��500��5      $	��600��5       $��0,-20q�5"      "  WpfAnimatedGif.ImageBehavior
   AnimatedSource$
 Resources/R.gif��5#      5%      �� 5'   
   $	��300��6   $	��500��5(      $��500,50q�5)      ���#2d333b$ 7��5+      ._�5,      �� $	��300��6   $	��500��6!   .+�5/      �� -   5         ��Name$ Play_Button��54      $��50��5/      $	��250��50      $��25,65q�51      $
��Play��52      ���#22272e   Launcher_Button_Style#��[  56      57      �� 5:      -   5        $ Github_Source_Code_Button��5>      $��50��5:      $	��250��5;      $��25,145q�5<      $��Github��5=      ���#22272e#��[  5A      5B      �� 5D      -   5        $ Tutorial_Button��5I      $��50��5D      $	��250��5E      $��25,225q�5F      $��Tutorial��5G      ���#22272e#��[  5K      5L      �� 5N      -	   5        $ Setting_Button��5S      $��50��5N      $	��250��5O      $��25,305q�5P      $��Settings��5Q      ���#22272e#��[  5U      5V      �� 5X      -
   5        $ Exit_Button��5]      $��50��5X      $	��250��5Y      $��25,385q�5Z      $
��Exit��5[      ���#22272e#��[  5_      5`      5a      5b      �� 5d   
   -   5        $ CurrentUserAvatarBorder��5k      $��50��5d      $��50��5e      $��2q�5f      ���#2d333b$	 100��5h      $��10q�5i      ._�5l      �� -   5        $ PlayerAccountCanvas��5l      $	��225��62   .+�5m      �� -   5         ��Name$ CurrentUserNameLabel��5t      $��	User Name��5m      $	��150��5n      $��50��5o      $
��Left=�5p      $��Center=�5q      
������$��20(�5s      $��55,-2.5q�5u      6+   5v      5w      �� 5x   
   -   5        $ Loading_Border��5{      $ 20��5x      
���   �$	��600��5z      $	��850��6   ._�5}      �� .+�5~        -   5          Name$ SpinnerLoad��5�      $ Spinner=�5~   "   
 ����$	��200��5      $	��200��5�         Spin$
 True��5�         SpinDuration$ 2Q�5�      $��325,200q�5�      5�   !   �� 5�      $��Authorization...��6   $��40(�5�      
������ �� Left$	 287��5�   0   
 �� Top$	 426��6B   5�      5�      5�      5�      