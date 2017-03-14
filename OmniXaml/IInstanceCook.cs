﻿namespace OmniXaml
{
    using System;
    using System.Collections.Generic;

    public interface IInstanceCook
    {
        CreationResult Create(Type constructionNodeInstanceType, IEnumerable<InjectableMember> injectableMembers);
    }
}