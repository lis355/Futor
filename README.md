<p align="center"><img src="Futor/Resources/ficon_enable.png" width=100 height=100></p>

# Futor
Output sound pitch changer on Windows, written on C#

## Install

If you want to use Futor, first, you must install virtual cable to switch you default output sound device to virtual device.

1. Download and install [VBCABLE_Driver_Pack43.zip](https://www.vb-audio.com/Cable/)

2. Switch default output sound device

<p align="center">
<img src="https://user-images.githubusercontent.com/2791094/45152831-cde31b80-b1da-11e8-966f-8f8c8bb22ebe.png">
</p>

3. Select this input virtual device and necessary output device

<p align="center">
<img src="https://user-images.githubusercontent.com/2791094/45153040-61b4e780-b1db-11e8-8c0b-9f6d85a10fc9.png">
</p>

<p align="center">
<img src="https://user-images.githubusercontent.com/2791094/45153102-89a44b00-b1db-11e8-9e8c-aa49e07bb999.png">
</p>

## Using

Now you can change output sound pitch via option "Pitch"

<p align="center">
<img src="https://user-images.githubusercontent.com/2791094/45153366-5f06c200-b1dc-11e8-81b9-5d19ce5de3a5.png">
</p>

Futor has hotkeys for fast pitch shifting:

* **Ctrl + Alt + Plus** - increase pitch by a semitone 
* **Ctrl + Alt + Plus** - decrease pitch by a semitone 
* **Ctrl + Alt + Multiply** - reset pitch 

Futor has option "Autostart" for starting on Windows starts

## Future

* Improve pitch shifting algorithm
* Use .NET VST for use VST plugins in stack
