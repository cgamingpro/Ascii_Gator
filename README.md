🎨 ASCII Generator

Turn your images into colorful ASCII art directly in your console! 🌈
Powered by C#, SixLabors.ImageSharp, and Sobel edge detection.

🖼️ Screenshots

<img width="572" height="729" alt="Screenshot 2025-10-16 161952" src="https://github.com/user-attachments/assets/3f83671d-8bf0-4291-a061-60ae09cf5748" /> <img width="583" height="726" alt="Screenshot 2025-10-16 155439" src="https://github.com/user-attachments/assets/474d13ed-14f0-4177-9986-c4b565c36f67" /> <img width="1278" height="951" alt="Screenshot 2025-10-16 160933" src="https://github.com/user-attachments/assets/8ed2ada7-80b3-4a73-921a-4fb5b36b63d3" /> ![s6](https://github.com/user-attachments/assets/a02e0849-092c-4546-bbde-127d53f6b456) <img width="1276" height="970" alt="Screenshot 2025-10-16 161001" src="https://github.com/user-attachments/assets/81ec9eab-7b7b-4811-9818-76ca5b42943e" />








✨ Features

🎨 Image Processing: Grayscale conversion, Gaussian blur, and Sobel edge detection.

🔍 Edge Detection: Highlights image details like lines and textures.

🌈 Color Mapping: Maps pixel colors to the nearest console colors for a vibrant look.

🆎 Custom ASCII Characters: Use your own character set for unique styles.

💻 Terminal Display: Show your ASCII art directly in your console with colors!

🛠️ Requirements

✅ .NET 6.0 or later

✅ SixLabors.ImageSharp NuGet package

📦 Installation

Clone the repository and restore dependencies:

git clone https://github.com/yourusername/ascii-generator.git
cd ascii-generator
dotnet restore

🚀 Usage

Run the application with the path to your image:

dotnet run -- "path/to/your/image.jpg"

🔧 Command-Line Options

--asciiChars – Custom ASCII character set (default: "@%#*+=-:. ").

--imagePath – Path to your input image.

🎯 Example
dotnet run -- --asciiChars " .:-=+*#%@" --imagePath "demoImage/s5.jpg"

⚡ How It Works

🖼️ Image Loading – Load images with SixLabors.ImageSharp.

⚪ Grayscale Conversion – Transform pixels to grayscale values.

💨 Gaussian Blur – Smooth out details.

🔎 Edge Detection – Detect edges using Sobel filters.

🌈 Color Mapping – Map original colors to nearest console colors.

🆎 ASCII Mapping – Convert brightness to ASCII characters.

💻 Terminal Output – Display the final colored ASCII art.

💡 Tips

Try different ASCII character sets for more creative results.

Resize large images to fit your terminal for best results.

Combine with foreground + background color blending for richer color depth.

📜 License

This project is licensed under the MIT License – see the LICENSE
 file for details.
