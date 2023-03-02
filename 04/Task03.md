## Task 4

###  Add JWT authentication to the WebAPI

1. Add support JWT authentication in the `Program.cs` and in app.settings.

```cs

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

```

```json
  "Jwt": {
      "Issuer": "https://test.com/",
      "Audience": "https://test.com/",
      "Key": "MFswDQYJKoZIhvcNAQEBBQADSgAwRwJAePcR0B4UaQfcn1MLARKuvbL40vRPolIHMvO+BExfjiUypkyQdRB2bHaR0IkRbM12Ac+3y9L2PNYPbp8VQzDMQIDAQAB"
  }
```

2. Use a proper command in the pipeline.

```cs
  app.UseAuthentication();
```

3. Create a service to handle JWT token. You can use the following code. Add a controller to get this token.

```cs  
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@example.com"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "your_audience",
                Issuer = "your_issuer"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
```

4. Allow Swagger to use support providing token.

```cs
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Oponeo WebAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
```


5. Use `[Authorize]` attribute on a method or controller and inspect if it works. What http error are you receiving? 
