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

# Build And Run
```
cd DHMiniSite
```
```
dotnet build
```
```
dotnet run
```
![This is an image](/screenshots/home.png)
![This is an image](/screenshots/posts.png)
