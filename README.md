# JwtTokenDemo

  Jwt'yi öğrenmek için yaptığım proje. .Net Core'un temel API projesi olan WeatherForecast'e yapılan isteklerin token gönderilerek yapılmasını sağlar.
Ayrıca eğer SqlExpress kullanıyorsanız veritabanı projeyi çalıştırdığınızda otomatik oluşur. Eğer SqlExpress kullanmıyorsanız ConnectionString alanını ve attribute'leri düzenlemeniz gerekir.

  Projeyi çalıştırdıktan sonra Postman gibi bir araç kullanarak önce, /api/account/login URL'ine POST'u seçerek {"UserName":"user1","Password":"1234"} gönderin, daha sonra gelen token ile /weatherforecast URL'ine GET isteği yapın. 
