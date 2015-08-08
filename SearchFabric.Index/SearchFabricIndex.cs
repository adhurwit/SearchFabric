using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services;
using System.Runtime.Serialization;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Microsoft.ServiceFabric.Data;
using Lucene.Net.Store;

namespace SearchFabric.Index
{
    [DataContract]
    public class TestDocument
    {
        [DataMember]
        public string Text;
        [DataMember]
        public int Id;
    }

    public interface ISearchFabricIndex : IService
    {
        Task<List<TestDocument>> Query(string q);
        Task Add(TestDocument doc);
    }
    

    public class SearchFabricIndex : StatefulService, ISearchFabricIndex
    {
        public Analyzer Analyzer = null;
        public ReliableRAMDirectory Index = null;

        public const string ServiceTypeName = "SearchFabricIndexType";

        protected override ICommunicationListener CreateCommunicationListener()
        {
            // may as well set up the objects here
            Analyzer = new WhitespaceAnalyzer();

            Index = new Lucene.Net.Store.ReliableRAMDirectory();
            ReliableRAMDirectory.StateManager = this.StateManager; 

            return new ServiceCommunicationListener<SearchFabricIndex>(this);
        }

        public Task Add(TestDocument doc)
        {
            var writer = new IndexWriter(Index, Analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            try
            {
                Document document = new Document();
                document.Add(new Field("id", doc.Id.ToString(), Field.Store.YES, Field.Index.NO));
                document.Add(new Field("text", doc.Text, Field.Store.YES, Field.Index.ANALYZED));
                writer.AddDocument(document);
            }
            catch(Exception e)
            {
                //
            }
            finally
            {
                //Close the writer
                writer.Commit();
                writer.Dispose();
            }
            return Task.FromResult(0);
        }

        public Task<List<TestDocument>> Query(string q)
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "text", Analyzer);
            Query query = parser.Parse(q);
            IndexSearcher searcher = new IndexSearcher(Index, true);
            //Do the search
            TopDocs docs = searcher.Search(query, 10);

            int results = docs.TotalHits;
            List<TestDocument> ret = new List<TestDocument>();
            for (int i = 0; i < results; i++)
            {
                ScoreDoc d = docs.ScoreDocs[i];
                float score = d.Score;
                Document idoc = searcher.Doc(d.Doc);
                ret.Add(new TestDocument()
                {
                    Id = Convert.ToInt32(idoc.GetField("id").StringValue),
                    Text = idoc.GetField("text").StringValue
                });
            }
            searcher.Dispose();
            
            return Task.FromResult(ret);
        }
    }
}
