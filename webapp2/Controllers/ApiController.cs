using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using irid.Models;
using Microsoft.AspNetCore.Mvc;

namespace irid.Controllers
{
    public class ApiController : Controller
    {
        private readonly IridDbContext _dbContext;
        private static SerialPort _serialPort;
        private static Task t;

        static ApiController()
        {
            _serialPort = new SerialPort
            {
                PortName = "/dev/ttyACM1",
                BaudRate = 38400,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                DtrEnable = false,
                ReadBufferSize = 131072
            };
            _serialPort.Open();
        }

        public ApiController(IridDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [HttpPost]
        [Route("/api/rotate")]
        public IActionResult Rotate(byte theta, byte phi)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Console.WriteLine(theta + " " + phi);
            if (!_serialPort.IsOpen) _serialPort.Open();
            _serialPort.Write(new[] {theta, phi}, 0, 2);
            return Ok();
        }

        [HttpGet]
        [HttpPost]
        [Route("/api/record")]
        public IActionResult Record()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (!_serialPort.IsOpen) _serialPort.Open();
            _serialPort.Write(new byte[] {0xff, 0}, 0, 2);
            var list = new List<byte>();
            int b = 0;
            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (!Task.Run(() =>
                    {
                        while ((b = _serialPort.ReadByte()) == -1) ;
                    }).Wait(1000)) goto br;
                    list.Add((byte) b);
                }
            }

            br:
            list.RemoveRange(Math.Max(list.Count - 2, 0), Math.Min(2, list.Count));
            Console.WriteLine("length=" + list.Count);
            if (list.Count < 1)
            {
                return Json(new
                {
                    status = "Timeout"
                });
            }

            return Json(new
            {
                status = "Success",
                value = Convert.ToBase64String(list.ToArray()),
                length = list.Count
            });
        }

        [HttpGet]
        [HttpPost]
        [Route("/api/operation")]
        public async Task<IActionResult> Operation(byte theta, byte phi, string data)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var bytes = Convert.FromBase64String(data);
            _serialPort.Write(new byte[] {theta, phi}, 0, 2);
            await Task.Delay(5000);
            _serialPort.Write(new byte[] {0, 255}, 0, 2);
            await Task.Delay(50);
            _serialPort.Write(bytes, 0, bytes.Length);
//            int offset = 0;
//            _serialPort.Write(bytes, offset, 32);
//            offset += 32;
//            var enumerator = bytes.GetEnumerator();
//            for (int i = 0; i < 16; i++)
//            {
//                if (!enumerator.MoveNext()) return Ok();
//                _serialPort.Write(new[] {(byte) enumerator.Current}, 0, 1);
//            }

//            while (offset < bytes.Length)
//            {
//                while (_serialPort.ReadByte() != 0) ;
//                _serialPort.Write(bytes, offset, Math.Min(32, bytes.Length - offset));
//                offset += 32;
////                _serialPort.Write(new[] {(byte) enumerator.Current}, 0, 1);
////                if (!enumerator.MoveNext()) break;
////                _serialPort.Write(new[] {(byte) enumerator.Current}, 0, 1);
//            }

            await Task.Delay(1000);
            _serialPort.Write(new byte[] {90, 90}, 0, 2);
            _serialPort.DiscardInBuffer();
            return Ok();
        }
    }
}