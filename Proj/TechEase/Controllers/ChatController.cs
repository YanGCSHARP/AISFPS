using System.Text.Json;
using IronXL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechEase.Models;

namespace TechEase.Controllers;


public class ChatController : ControllerBase
{

    //Task<List<List<Product>>>
    [EnableCors]
    [AllowAnonymous]
    [HttpPost("/ask")]
    public async Task<IActionResult> Asking([FromBody] string question)
    {
        var chat = new AmChat();
        var answer = await chat.GetAnswer(question);
        
        // promt написать
        WorkBook workBook = WorkBook.Load("C:\\Aboba\\test.xlsx");
        WorkSheet sheet = workBook.WorkSheets.First();
        
        if (answer[0].ToString() == "1")
        {
            string[] str = answer.Split('\n').Skip(1).ToArray();
            
            var product1 = new Product { Name = str[0], Image = "", Price = "", Description = ""};
            Livenstein livenstein = new Livenstein(sheet, str[0]);
            var row = livenstein._row;

            product1.Description = sheet[$"C{row}"].First().ToString();
            product1.Price = sheet[$"D{row}"].First().ToString();
            product1.Image = sheet[$"E{row}"].First().ToString();
            
            var product2 = new Product { Name = str[1], Image = "", Price = "", Description = ""};
            Livenstein livenstein2 = new Livenstein(sheet, str[1]);
            var row2 = livenstein2._row;

            product2.Description = sheet[$"C{row2}"].First().ToString();
            product2.Price = sheet[$"D{row2}"].First().ToString();
            product2.Image = sheet[$"E{row2}"].First().ToString();
            
            var product3 = new Product { Name = str[2], Image = "", Price = "", Description = ""};
            Livenstein livenstein3 = new Livenstein(sheet, str[2]);
            var row3 = livenstein3._row;

            product3.Description = sheet[$"C{row3}"].First().ToString();
            product3.Price = sheet[$"D{row3}"].First().ToString();
            product3.Image = sheet[$"E{row3}"].First().ToString();
            
            
            List<Product> listt = new List<Product>();
            listt.Add(product1);
            listt.Add(product2);
            listt.Add(product3);
            
            List<List<Product>> mainList = new List<List<Product>>();
            return Ok(mainList);
            mainList.Add(listt);
        } else 
        {
            string[] str1 = answer.Split('\n').Skip(1).ToArray();
            string[][] str = str1.Select(line => line.Split(" / ")).ToArray();


            List<Product> listt = new List<Product>();
            List<List<Product>> mainList = new List<List<Product>>();
            for (int i = 0; i < 3; i++)
            {
                var product1 = new Product { Name = str[i][0], Image = "", Price = "", Description = ""};
                Livenstein livenstein = new Livenstein(sheet, str[0][0]);
                var row = livenstein._row;
            
                product1.Description = sheet[$"C{row}"].First().ToString();
                product1.Price = sheet[$"D{row}"].First().ToString();
                product1.Image = sheet[$"E{row}"].First().ToString();
            
                var product2 = new Product { Name = str[i][1], Image = "", Price = "", Description = ""};
                Livenstein livenstein2 = new Livenstein(sheet, str[1][0]);
                var row2 = livenstein2._row;
            
                product2.Description = sheet[$"C{row2}"].First().ToString();
                product2.Price = sheet[$"D{row2}"].First().ToString();
                product2.Image = sheet[$"E{row2}"].First().ToString();
            
                var product3 = new Product { Name = str[i][2], Image = "", Price = "", Description = ""};
                Livenstein livenstein3 = new Livenstein(sheet, str[2][0]);
                var row3 = livenstein3._row;
            
                product3.Description = sheet[$"C{row3}"].First().ToString();
                product3.Price = sheet[$"D{row3}"].First().ToString();
                product3.Image = sheet[$"E{row3}"].First().ToString();
                
                listt.Add(product1);
                listt.Add(product2);
                listt.Add(product3);
                mainList.Add(listt);
                listt = new List<Product>();
            }
            return Ok(mainList);
        }
        
        
        
        if (answer[0].ToString() == "1")
        {
            
            
            
        } else if (answer[0].ToString() == "2")
        {
            
        }
        
        
        
        // названия полученные через метод ливенштейна сравнить и найти в базе
        // вернуть либо 1) сборку их множества продуктов 2) 3 продукта
        
        //вернуть в виде json

        return null;
    }
    
    [AllowAnonymous]
    [HttpGet("/getcell")]
    public async Task<string> GetCell()
    {
        WorkBook workbook = WorkBook.Load("C:\\Aboba\\test.xlsx");
        WorkSheet sheet =  workbook.WorkSheets.First();
        var cell = sheet["B2"].First();
        return cell.ToString();
    }

    [AllowAnonymous]
    [HttpGet("/getproduct")]
    public async Task<Product> GetProduct(string name)
    {
        WorkBook workBook = WorkBook.Load("C:\\Aboba\\test.xlsx");
        WorkSheet sheet = workBook.WorkSheets.First();

        var product = new Product { Name = name, Image = "", Price = "", Description = ""};
        Livenstein livenstein = new Livenstein(sheet, name);
        var row = livenstein._row;

        product.Description = sheet[$"C{row}"].First().ToString();
        product.Price = sheet[$"D{row}"].First().ToString();
        product.Image = sheet[$"E{row}"].First().ToString();
        
        return product;
    }
    
    
    [AllowAnonymous]
    [HttpGet("/get-lists")]
    public async Task<string> GetLists()
    {
        
        


        return "txt";
    }
    
    
    
}
