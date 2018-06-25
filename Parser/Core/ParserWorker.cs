using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        public HtmlLoader loader;

        #region Properties

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;                
            }
        }

        public HtmlLoader Loader
        {
            get
            {
                return loader;
            }
            private set
            {
                loader = value;
            }
        }
        #endregion

        //public ParserWorker(IParser<T> parser)
        //{
        //    this.parser = parser;
        //}

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) //: this(parser)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
            loader = new HtmlLoader(parserSettings);
        }
    }
}
