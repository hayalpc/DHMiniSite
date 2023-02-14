# DHMiniSite
- MongoDB configuration in appsettings.json
```
  "DHMiniSiteDatabase": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "dh_mini_site",
    "PostsCollectionName": "posts",
    "CommentsCollectionName": "comments"
  }
```

- Endpoint configuration in appsettings.json
```
"Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://*:8001"
      },
      "Http": {
        "Url": "http://*:8000"
      }
    }
  }
```

- RabbitMQ configuration in appsettings.json
```
  "RabbitMQConfiguration": {
    "HostName": "localhost",
    "Password": "guest",
    "UserName": "guest"
  }
```
# DHRabbitMQConsumer
- SMTP configuration in appsettings.json
```
  "SmtpConfig": {
    "Host": "smtp.yandex.com.tr",
    "Port": "465",
    "User": "info@giagames.co",
    "From": "info@giagames.co",
    "Password": "qmjiabczulimuyva",
    "UseSSL": "true"
  }
```

- RabbitMQ configuration in appsettings.json
```
  "RabbitMQConfiguration": {
    "HostName": "localhost",
    "Password": "guest",
    "UserName": "guest"
  }
```



# Build And Run
- For DHMiniSite project
```
cd DHMiniSite
dotnet build
dotnet run
```

- For DHRabbitMQConsumer project
```
cd DHRabbitMQConsumer
dotnet build
dotnet run
```

# Some Screenshots
- Home page
![This is an image](/screenshots/home.png)
- Posts
![This is an image](/screenshots/posts.png)
- Post detail with comment
![This is an image](/screenshots/post_detail_with_comment.png)
- Post edit
![This is an image](/screenshots/post_edit.png)
- Comments
![This is an image](/screenshots/comments.png)
