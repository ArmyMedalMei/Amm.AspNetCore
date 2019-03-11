# Amm.AspNetCore
Amm.AspNetCore项目（方便自用）旨在于快速构建.NETCore项目。可以用于MVC和WebApi,其中涵盖了自定义错误验证、视图验证、IOC、AutoMapper、Log4net、Redis、JWT授权、SwaggerUi、MongDb和一些常用的基础方法

# 配置Startup.cs：

        /// <summary>
        ///     此方法由运行时调用。使用此方法向容器添加服务。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 使用实体自动映射

            services.AddAutoMapper();

            #endregion

            #region 注入数据库仓储

            //使用dapper进行数据仓储的封装，默认使用的SQLSERVER
            services.UseDapper(o =>
            {
                o.ConnectionString = Configuration.GetSection("ConnectionStrings:SqlServerConnection").Value;
                o.DbType = DbType.MsSqlServer;
            });

            #endregion

            #region 添加加密配置文件

            services.Configure<EncryptionOptions>(options =>
            {
                options.Key = Configuration.GetSection("Encrypt:Key").Value;
            });

            #endregion

            #region 添加log4net日志记录

            services.AddLog4Net();

            #endregion

            #region 添加AspNetCore模块

            services.AddHttpContextAccessor();
            services.AddAspNetCore();

            #endregion

            #region 使用redis

            services.AddRedis(options =>
            {
                options.ConnectionString = Configuration.GetSection("Cache:ConnectionString").Value;
                options.DataBaseIndex = Convert.ToInt32(Configuration.GetSection("Cache:DataBaseIndex").Value);
            });

            #endregion 

            #region 添加jwt认证

            //获取认证配置
            var audienceConfig = Configuration.GetSection("Audience");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //不使用https
                    options.RequireHttpsMetadata = false;
                    //有默认值，具体参考TokenValidationParameters类
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //这个属性至关重要，设置服务器过期时间偏移量
                        ClockSkew = TimeSpan.FromSeconds(20),
                        //Audience
                        ValidAudience = audienceConfig["Audience"],
                        //Issuer，这两项和前面签发jwt的设置一致
                        ValidIssuer = audienceConfig["Issuer"],
                        //SecurityKey
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(audienceConfig["SecurityKey"]))
                    };
                });

            #endregion

            #region 使用MVC

            services.AddMvc(options =>
                    {
                        //自定义过滤器
                        options.Filters.Add<AmmActionFilter>();
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    //全局配置Json序列化处理
                    .AddJsonOptions(options =>
                        {
                            //忽略循环引用
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            //不使用驼峰样式的key
                            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                            //设置时间格式
                            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                        }
                    );
            #endregion

            #region 使用自定义授权认证

            services.AddAmmAuthorization(options =>
                   {
                       options.IsEnableAuthrization = Convert.ToBoolean(Configuration.GetSection("Authorization:IsEnableAuthrization").Value);
                   });
            #endregion

            #region 使用swaggerui

            //添加swaggerui配置
            services.Configure<SwaggerUiOptions>(options =>
            {
                options.SwaggerEndpointUrl = Configuration.GetSection("SwaggerUi:SwaggerEndpoint").Value;
            });
            services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(Configuration.GetSection("SwaggerUi:Version").Value, new Info
                    {
                        Version = Configuration.GetSection("SwaggerUi:Version").Value,
                        Title = Configuration.GetSection("SwaggerUi:Title").Value,
                        Description = Configuration.GetSection("SwaggerUi:Description").Value
                    });
                    options.DocInclusionPredicate((docName, description) => true);
                    //自定义架构ID
                    options.CustomSchemaIds(x => x.FullName);
                    //将当前所有的XML添加到注释中，为 Swagger JSON and UI设置xml文档注释路径
                    Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                    {
                        options.IncludeXmlComments(file, true);
                    });
                    //权限Token
                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });
                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", Enumerable.Empty<string>() }
                    });
                });

            #endregion
        }
        
# appsettings.json文件配置如下：
  `{
  "Logging": {
    "LogLevel": {
      "Default": "Error",
      "System": "Error",
      "Microsoft": "Error"
    }
  },
  "Authorization": {
    "IsEnableAuthrization": false
  },
  "SwaggerUi": {
    "SwaggerEndpoint": "/swagger/v1.0/swagger.json",
    "Version": "v1.0",
    "Title": "api接口文档",
    "Description": "这是一个公用的restful文档格式的api接口"
  },
  "AllowedHosts": "*",
  "Encrypt": {
    "Key": "EShaihukE9tI7NcNZ26imFTg"
  },
  "Cache": {
    "DataBaseIndex": "0",
    "ConnectionString": "xxxxx:6379,password=xxxxxxx"
  },
  "MongoDb": {
    "IsEnabledAuthorization": false,
    "DataBase": "jwellface",
    "UserName": "jwellface",
    "Password": "jwellface123!##",
    "ConnectionString": "127.0.0.1:27017"
  },
  "ConnectionStrings": {
    "SqlServerConnection": "server=xxx;database=xxx;uid=xxx;pwd=xxxxx;multipleactiveresultsets=true;pooling=true;min pool size=0;max pool size=10000",
    "PgSqlConnection": "User ID=Amm;Password=xxx;Host=xxxxx;Port=5432;Database=xxxx;Pooling=true;MinPoolSize=0;MaxPoolSize=100"
  },
  "Audience": {
    "SecurityKey": "Y2F0Y2yhciUyMHdvbmclMFWfsaZlJTIwLm5ldA==",
    "Issuer": "AmmNetworkMark",
    "Audience": "AmmNetworkMark"
  }
}`
