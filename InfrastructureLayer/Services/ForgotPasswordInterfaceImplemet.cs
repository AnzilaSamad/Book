using ApplicationLayer.DTO;
using ApplicationLayer.Interface;
using InfrastructureLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DomainLayer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Data.Entity;


namespace InfrastructureLayer.Services
{

    public class ForgotPasswordInterfaceImplemet : IForgotPassword
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRegisterValidation _registerValidation;

        public ForgotPasswordInterfaceImplemet(AppDbContext appDbContext, IRegisterValidation registerValidation)
        {
            _appDbContext = appDbContext;
            _registerValidation = registerValidation;

        }

        public string ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {

            var user = _appDbContext.UserRegisterDetails.FirstOrDefault(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                return "Email not found";
            }

            else
            {
                var password = _registerValidation.CreateRandomPassword();

                _registerValidation.CreatePasswordHash(password, out byte[] PasswordHash, out byte[] PasswordSalt);

                PasswordChangeDto temp = new PasswordChangeDto()
                {
                   
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,
                    Email= forgotPasswordDto.Email,

                };
                _registerValidation.Edit(temp);
                

                       



                MailMessage mm = new MailMessage();
                //mm.ReplyTo = new MailAddress("anzilasamad98@gmail.com");
                mm.From = new MailAddress("bookverse.theuniverse@gmail.com");
                mm.To.Add(user.Email);
                mm.Subject = "Password Recovery";
                mm.Body = string.Format("Hi {0},<br /><br />Your password is {1}.<br /><br />Thank You.", user.Username, password);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "bookverse.theuniverse@gmail.com";
                NetworkCred.Password = "tpwbdyntuwccfdva";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
            return "Email sent";



        }
        }
    }

