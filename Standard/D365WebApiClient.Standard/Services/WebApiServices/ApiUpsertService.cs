﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using D365WebApiClient.Standard.Common;
using D365WebApiClient.Standard.WebApiQueryOptions;
using D365WebApiClient.Values;
using Newtonsoft.Json;

namespace D365WebApiClient.Standard.Services.WebApiServices
{
    public partial class ApiClientService
    {
        /// <summary>
        ///     插入/更新
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="guid"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <returns></returns>
        public async Task UpsertAsync(string entityName, Guid guid, Value value, bool ifMatch = false,
            bool ifNoneMatch = false)
        {
            var url = BuildGuidUrl(entityName, guid);

            var req = BuildRequest(new HttpMethod("PATCH"), url, value);

            req = BuildIfMatchAndNoneMatch(req, ifMatch, ifNoneMatch);

            var response = await SendAsync(req); //204
        }

        /// <summary>
        ///     插入/更新
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKey"></param>
        /// <param name="alternateValue"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <returns></returns>
        public async Task UpsertAsync(string entityName, string alternateKey, string alternateValue, Value value,
            bool ifMatch = false, bool ifNoneMatch = false)
        {
            await UpsertAsync(entityName, new[] { new KeyValuePair<string, string>(alternateKey, alternateValue) },
                value, ifMatch, ifNoneMatch);
        }

        /// <summary>
        ///     插入/更新
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKeyValues"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <returns></returns>
        public async Task UpsertAsync(string entityName, IEnumerable<KeyValuePair<string, string>> alternateKeyValues,
            Value value,
            bool ifMatch = false, bool ifNoneMatch = false)
        {
            var url = BuildAlternateKeyUrl(entityName, alternateKeyValues);

            var req = BuildRequest(new HttpMethod("PATCH"), url, value);


            req = BuildIfMatchAndNoneMatch(req, ifMatch, ifNoneMatch);

            var response = await SendAsync(req); //204
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="guid"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName, Guid guid, Value value,
            QueryOptions queryOptions, bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            return await UpsertAndReadAsync(entityName, guid, value, queryOptions?.ToString(), ifMatch, ifNoneMatch,
                enumAnnotations);
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="guid"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName, Guid guid, Value value,
            string queryOptions, bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            var url = BuildGuidUrl(entityName, guid, queryOptions);

            var req = BuildRequest(new HttpMethod("PATCH"), url, value, enumAnnotations, null, true);

            req = BuildIfMatchAndNoneMatch(req, ifMatch, ifNoneMatch);

            var response = await SendAsync(req); //200

            //Body should contain the requested new-contact information.
            var deserializeObject = Value.Read(
                await response.Content.ReadAsStringAsync());
            return deserializeObject;
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKey"></param>
        /// <param name="alternateValue"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName,
            string alternateKey, string alternateValue,
            Value value, QueryOptions queryOptions,
            bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            return await UpsertAndReadAsync(entityName,
                alternateKey,
                alternateValue,
                value,
                queryOptions?.ToString(),
                ifMatch, ifNoneMatch,
                enumAnnotations);
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKey"></param>
        /// <param name="alternateValue"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName,
            string alternateKey, string alternateValue,
            Value value, string queryOptions,
            bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            return await UpsertAndReadAsync(entityName,
                new[] { new KeyValuePair<string, string>(alternateKey, alternateValue) },
                value,
                queryOptions,
                ifMatch, ifNoneMatch,
                enumAnnotations);
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKeyValues"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName,
            IEnumerable<KeyValuePair<string, string>> alternateKeyValues,
            Value value, QueryOptions queryOptions,
            bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            return await UpsertAndReadAsync(entityName, alternateKeyValues, value, queryOptions?.ToString(), ifMatch,
                ifNoneMatch, enumAnnotations);
        }

        /// <summary>
        ///     插入/更新 并查询 仅v8.2
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="alternateKeyValues"></param>
        /// <param name="queryOptions"></param>
        /// <param name="value"></param>
        /// <param name="ifMatch">
        ///     如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加 If-Match 标题到值为 "*" 的请求。 如果更新数据，并且有某种可能会有意删除实体，则您不想再创建实体。 若要防止出现此情况，添加
        ///     If-Match 标题到值为 "*" 的请求。
        ///     <para>如果找到实体，您将收到状态 204 的正常响应 (No Content)。 如果未找到实体，您将收到状态 404 的以下响应 (Not Found)。</para>
        /// </param>
        /// <param name="ifNoneMatch">
        ///     如果在插入数据，有某种可能具有相同 id 值的记录已存在于系统中，您可能不希望更新它。 若要防止出现此情况，添加 If-None-Match 标题到值为 "*" 的请求。
        ///     <para>如果未找到实体，您将收到状态 204 的正常响应 (No Content)。 如果找到实体，您将收到状态 412 的以下响应 (Precondition Failed)。</para>
        /// </param>
        /// <param name="enumAnnotations"></param>
        /// <returns></returns>
        public async Task<Value> UpsertAndReadAsync(string entityName,
            IEnumerable<KeyValuePair<string, string>> alternateKeyValues,
            Value value, string queryOptions,
            bool ifMatch = false, bool ifNoneMatch = false,
            EnumAnnotations enumAnnotations = EnumAnnotations.None)
        {
            var url = BuildAlternateKeyUrl(entityName, alternateKeyValues, queryOptions);

            var req = BuildRequest(new HttpMethod("PATCH"), url, value, enumAnnotations, null, true);

            req = BuildIfMatchAndNoneMatch(req, ifMatch, ifNoneMatch);

            var response = await SendAsync(req); //200

            //Body should contain the requested new-contact information.
            var deserializeObject = Value.Read(
                await response.Content.ReadAsStringAsync());
            return deserializeObject;
        }

        private HttpRequestMessage BuildIfMatchAndNoneMatch(HttpRequestMessage req,
            bool ifMatch = false, bool ifNoneMatch = false)
        {
            if (ifMatch && ifNoneMatch) throw new Exception("不能同时存在，无法Upsert");

            if (ifMatch) req.Headers.IfMatch.Add(EntityTagHeaderValue.Any);

            if (ifNoneMatch) req.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Any);

            return req;
        }
    }
}