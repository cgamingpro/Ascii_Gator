ASCII Generator

A C# console application that converts images into colorized ASCII art, utilizing edge detection and Sobel filtering for enhanced visual representation.


<img width="572" height="729" alt="Screenshot 2025-10-16 161952" src="https://github.com/user-attachments/assets/3f83671d-8bf0-4291-a061-60ae09cf5748" />
<img width="583" height="726" alt="Screenshot 2025-10-16 155439" src="https://github.com/user-attachments/assets/474d13ed-14f0-4177-9986-c4b565c36f67" />


<img width="1278" height="951" alt="Screenshot 2025-10-16 160933" src="https://github.com/user-attachments/assets/8ed2ada7-80b3-4a73-921a-4fb5b36b63d3" />

![s6](https://github.com/user-attachments/assets/a02e0849-092c-4546-bbde-127d53f6b456)

<img width="1276" height="970" alt="Screenshot 2025-10-16 161001" src="https://github.com/user-attachments/assets/81ec9eab-7b7b-4811-9818-76ca5b42943e" />




Features

Image Processing: Converts images to grayscale, applies Gaussian blur, and detects edges using Sobel filters.

Edge Detection: Utilizes Sobel filters to identify edges and enhance image details.

Color Mapping: Maps pixel colors to the nearest console colors for terminal display.

Customizable ASCII Characters: Allows customization of ASCII character sets for different visual effects.

Terminal Display: Outputs the generated ASCII art directly to the console with color support.

Requirements

.NET 6.0 or later

SixLabors.ImageSharp NuGet package

Installation

Clone the repository and restore dependencies:

git clone https://github.com/yourusername/ascii-generator.git
cd ascii-generator
dotnet restore

Usage

Run the application with the path to your image:

dotnet run -- "path/to/your/image.jpg"

Command-Line Options

--asciiChars: Custom ASCII character set (default: "@%#*+=-:. ").

--imagePath: Path to the input image.

Example
dotnet run -- --asciiChars " .:-=+*#%@" --imagePath "demoImage/s5.jpg"

How It Works

Image Loading: Loads the image using SixLabors.ImageSharp.

Grayscale Conversion: Converts the image to grayscale.

Gaussian Blur: Applies a Gaussian blur to smooth the image.

Edge Detection: Detects edges using Sobel filters.

Color Mapping: Maps pixel colors to the nearest console colors.

ASCII Mapping: Maps pixel brightness to ASCII characters.

Terminal Output: Displays the generated ASCII art in the terminal.
