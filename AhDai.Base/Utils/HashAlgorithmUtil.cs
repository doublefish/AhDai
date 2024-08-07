﻿using System.Security.Cryptography;

namespace AhDai.Base.Utils
{
    /// <summary>
    /// HashAlgorithmHelper
    /// </summary>
    public static class HashAlgorithmUtil
    {
        /// <summary>
        /// 使用using调用ComputeHash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ComputeHashByUsing<T>(byte[] buffer) where T : HashAlgorithm, new()
        {
            using var hash = new T();
            return hash.ComputeHash(buffer);
        }
    }
}
