<!--Category:Powershell--> 
 <p align="right">
    <a href="https://www.powershellgallery.com/packages/ProductivityTools.ConvertTcx2Gpx/"><img src="Images/Header/Powershell_border_40px.png" /></a>
    <a href="http://productivitytools.tech/import-modulesfromdirectory/"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://github.com/pwujczyk/ProductivityTools.ConvertTcx2Gpx"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>


# Convert TCX to GPX

Module performs bulk conversion from TCX files to GPX files. 
<!--more-->
Module was created to help move sport activities from Endomondo to another portal. It takes directory with TCX files and convert every file to GPX. 


```powershell
Convert-Tcx2Gpx -Path d:\Trash\Workouts\ -Verbose
```

Module first will download **GPSBabel** application which is used to perform conversion. application is stored directly in the GitHub.

<!--og-image-->
![Download and extract Babel](Images/DownloadAndExtract.png)

Next it will extract it and start the conversion.

![Download and extract Babel](Images/Convert.png)