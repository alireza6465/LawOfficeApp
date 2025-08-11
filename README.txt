پکیج سورس — Law Office Manager (WinForms + SQLite)

این بسته شامل سورس پروژهٔ WinForms که با SQLite کار می‌کند است و اسکریپت‌های لازم برای تولید یک فایل اجرایی تک‌فایلی و نصاب Setup.exe.
هدف: تولید یک Setup.exe نهایی که نیازی به نصب نرم‌افزار اضافی (مثل SQL Server) نداشته باشد و دیتابیس محلی در Documents ذخیره شود.

فایل‌های مهم:
- src/LawOfficeApp.csproj  - پروژه
- src/Program.cs
- src/Database.cs
- src/DataAccess.cs
- src/MainForm.cs
- src/ClientForm.cs
- src/CaseForm.cs, src/CheckForm.cs, src/ReminderForm.cs (اسکلت)
- installer_scripts/lawoffice_installer.iss - اسکریپت Inno Setup برای ساخت نصب‌کننده
- installer_scripts/build.ps1 - اسکریپت محلی برای build + publish + ساخت installer (در صورت نصب Inno Setup)
- .github/workflows/build.yml - گردش کار GitHub Actions برای ساخت خودکار (CI) و تولید artifact

دو راه سریع برای گرفتن Setup.exe (بدون نیاز به Visual Studio روی کامپیوتر خودت):
A) استفاده از GitHub Actions (توصیه شده برای راحتی)
  1. یک مخزن جدید در GitHub بساز (نام دلخواه).
  2. فایل‌های این پوشه را به ریشهٔ مخزن کامیت و پوش کن.
  3. در صفحه GitHub Actions، اجرای گردش‌کار را صبر کن یا دستی اجرا کن (Actions -> Build and Package).
  4. پس از اجرای موفق، در بخش Artifacts یا در صفحه اجرای workflow، می‌توانی خروجی publish و فایل نصاب (*.exe) را دانلود کنی.

B) ساخت محلی (اگر ترجیح می‌دهی روی سیستم خودت باشد)
  پیش‌نیازها:
  - Windows
  - .NET 7 SDK نصب‌شده (از https://dotnet.microsoft.com)
  - (اختیاری برای ساخت نصاب) Inno Setup Compiler (https://jrsoftware.org/isinfo.php)
  مراحل:
  1. پوشه را روی سیستم ویندوزی اکسترکت کن.
  2. در PowerShell به مسیر ریشهٔ این بسته برو.
  3. اجرا کن: `.\installer_scripts\build.ps1`
     - این اسکریپت ابتدا `dotnet publish` را اجرا می‌کند و خروجی را در پوشه `publish` می‌گذارد.
     - اگر Inno Setup نصب داشته باشی، اسکریپت سپس ISCC را اجرا کرده و فایل نصبی را می‌سازد.
  4. فایل نصبی (LawOffice_Setup.exe) در مسیر پروژه ساخته خواهد شد (در همان پوشه). آن را اجرا کن تا نصب شود.

نکته‌های مهم:
- دیتابیس در `%USERPROFILE%\Documents\LawOfficeData\LawOfficeData.db` قرار می‌گیرد. این فایل هنگام نصب در صورت وجود استفاده می‌شود و در صورت عدم وجود یک دیتابیس اولیه کپی می‌شود.
- برای نگه داشتن اطلاعات در هنگام آپدیت، کافی است نسخهٔ جدید برنامه را نصب کنی؛ دیتابیس در Documents جدا نگهداری می‌شود و حذف نخواهد شد.
- پس از نصب، شورت‌کات در منوی Start و دسکتاپ ایجاد می‌شود.

اگر می‌خواهی من همین حالا این بسته را برایت ZIP کنم و لینک دانلود بدهم، تایید کن تا فایل ZIP را آماده و ارسال کنم.
همچنین می‌تونم در صورت تمایل مرحله به مرحله (با تصویر) راهنمای نصب GitHub Actions یا اجرای محلی را برایت آماده کنم.
