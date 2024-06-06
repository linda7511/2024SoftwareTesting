using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DbOracle.Entities;
using DbOracle.Models;
using DbOracle.SQL;
using DbOracle.Repository;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace DbOracle
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
			/*
			services.AddControllers(options =>
			{
				options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build()));
			})
				.AddJsonOptions(options =>
				 {
					 options.JsonSerializerOptions.PropertyNamingPolicy = null;
				 });
			*/
			services.AddTransient<TokenManager>();
			services.AddSingleton<IMemoryCache, MemoryCache>();
			services.AddDbContextPool<MyDbContext>(
				options => options.UseOracle(_configuration.GetConnectionString("StudentDBconnection"))
				);
			services.AddMvc(options => options.EnableEndpointRouting = false);
			

            services.AddScoped<IAccountingSubjectRepository, SQLAccountingSubjectRepository>();
            services.AddScoped<IApplicationRepository, SQLApplicationRepository>();
            services.AddScoped<IAssetsInformationRepository, SQLAssetsInformationRepository>();
            services.AddScoped<IAssetsSummarizeRepository, SQLAssetsSummarizeRepository>();
            services.AddScoped<IAssignmentRepository, SQLAssignmentRepository>();
            services.AddScoped<IAttendanceInformationRepository, SQLAttendanceInformationRepository>();
            services.AddScoped<IBillRepository, SQLBillRepository>();
            services.AddScoped<IBookRepository, SQLBookRepository>();
            services.AddScoped<ICarRepository, SQLCarRepository>();
            services.AddScoped<ICheckinRepository, SQLCheckinRepository>();
            services.AddScoped<ICheckInCheckOutRepository, SQLCheckInCheckOutRepository>();
            services.AddScoped<ICheckrepairRepository, SQLCheckrepairRepository>();
            services.AddScoped<ICleaningRepository, SQLCleaningRepository>();
            services.AddScoped<IComplaintRepository, SQLComplaintRepository>();
            services.AddScoped<IConsumeRepository, SQLConsumeRepository>();
            services.AddScoped<IConsumptionRecordRepository, SQLConsumptionRecordRepository>();
            services.AddScoped<ICustomerRepository, SQLCustomerRepository>();
            services.AddScoped<IDepartmentRepository, SQLDepartmentRepository>();
            services.AddScoped<IDishRepository, SQLDishRepository>();


            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddScoped<IEnterExitRepository, SQLEnterExitRepository>();
            services.AddScoped<IGoodRepository, SQLGoodRepository>();
            services.AddScoped<ILocationRepository, SQLLocationRepository>();
            services.AddScoped<IMaintainRepository, SQLMaintainRepository>();
            services.AddScoped<IMyorderRepository, SQLMyorderRepository>();
            services.AddScoped<IMytableRepository, SQLMytableRepository>();
            services.AddScoped<IParkingRepository, SQLParkingRepository>();
            services.AddScoped<IParkPlaceRepository, SQLParkPlaceRepository>();
            services.AddScoped<IPayRepository, SQLPayRepository>();
            services.AddScoped<IPostRepository, SQLPostRepository>();
            services.AddScoped<IPurchaseRepository, SQLPurchaseRepository>();
            services.AddScoped<IRoomRepository, SQLRoomRepository>();
            services.AddScoped<IRoomorderRepository, SQLRoomorderRepository>();
            services.AddScoped<IRoomtypeRepository, SQLRoomtypeRepository>();
            services.AddScoped<ISalaryRepository, SQLSalaryRepository>();
            services.AddScoped<IScrapRepository, SQLScrapRepository>();
            services.AddScoped<IServiceRepository, SQLServiceRepository>();
            services.AddScoped<IStagingRepository, SQLStagingRepository>();
            services.AddScoped<ISupplierRepository, SQLSupplierRepository>();
            services.AddScoped<ISupplierRepository, SQLSupplierRepository>();
            services.AddScoped<IUnitPriceRepository, SQLUnitPriceRepository>();


            services.AddEndpointsApiExplorer();
			services.AddCors(cors =>
			{
				cors.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			});
			services.AddSwaggerGen();
			services.AddSwaggerGen(
				s => {
					//添加安全定义
					s.AddSecurityDefinition("Bearer",
						new OpenApiSecurityScheme
						{
							Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
							Name = "Authorization",
							In = ParameterLocation.Header,
							Type = SecuritySchemeType.ApiKey,
							BearerFormat = "JWT",
							Scheme = "Bearer"
						}
					);
					//添加安全要求
					s.AddSecurityRequirement(new OpenApiSecurityRequirement {
							{ new OpenApiSecurityScheme{
								Reference =new OpenApiReference{
									Type = ReferenceType.SecurityScheme,
									Id ="Bearer"
								}
							  },
							  new string[]{ }
							}
						}
					);

					s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "myproject.xml"));
				}
			);

			services.Configure<JwtConfig>(_configuration.GetSection("JWT"));
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
				opt => {
					var JwtOtp = _configuration.GetSection("JWT").Get<JwtConfig>();
					byte[] keybase = Encoding.UTF8.GetBytes(JwtOtp.Key);
					var seckey = new SymmetricSecurityKey(keybase);
					opt.TokenValidationParameters = new()
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = seckey,
						ClockSkew = TimeSpan.Zero,
					};
					opt.Events = new JwtBearerEvents
					{
						//权限验证失败后执行
						OnChallenge = context => {
							//终止默认的返回结果
							context.HandleResponse();
							string token = context.Request.Headers["Authorization"];
							var result = JsonConvert.SerializeObject(new { code = 401, message = "登录过期" });
							if (string.IsNullOrEmpty(token))
							{
								result = JsonConvert.SerializeObject(new { code = 401, message = "token不能为空" });
								context.Response.ContentType = "application/json";
								//验证失败返回401
								context.Response.StatusCode = StatusCodes.Status200OK;
								context.Response.WriteAsync(result);
								return Task.FromResult(result);
							}
							try
							{
								JwtSecurityTokenHandler tokenheader = new();
								ClaimsPrincipal claimsPrincipal = tokenheader.ValidateToken(token, opt.TokenValidationParameters, out SecurityToken securityToken);
							}
							catch (SecurityTokenExpiredException)
							{
								result = JsonConvert.SerializeObject(new { code = 401, message = "登录已过期" });
								context.Response.ContentType = "application/json";
								//验证失败返回401
								context.Response.StatusCode = StatusCodes.Status200OK;
								context.Response.WriteAsync(result);
								return Task.FromResult(result);
							}
							catch (Exception ex)
							{
								Console.WriteLine("token令牌无效  " + ex.GetType().Name);
								result = JsonConvert.SerializeObject(new { code = 402, message = "token令牌无效" });
								context.Response.ContentType = "application/json";
								//验证失败返回401
								context.Response.StatusCode = StatusCodes.Status200OK;
								context.Response.WriteAsync(result);
								return Task.FromResult(result);
							}
							context.Response.ContentType = "application/json";
							//验证失败返回401
							context.Response.StatusCode = StatusCodes.Status200OK;
							context.Response.WriteAsync(result);
							return Task.FromResult(result);
						}
					};
				}
			);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();

			app.UseRouting();
			app.UseCors("any");

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
