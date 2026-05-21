using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.ValueObjects
{
    public class PlanFeatures
    {

        public bool CanUseCustomDomain { get; private set; } // هل يسمح للعميل باستخدام دومين خاص به أم لا
        public bool HasAiRecommendations { get; private set; } // هل يحصل العميل على توصيات ذكية تعتمد على الذكاء الاصطناعي لتحسين مبيعاته وزيادة أرباحه
        public bool HasAdvancedReports { get; private set; } // هل يحصل العميل على تقارير وتحليلات متقدمة عن مبيعاته وأداء متجره
        public bool HasPrioritySupports { get; private set; } // هل يحصل العميل على دعم فني مميز وأولوية في الرد على استفساراته ومشاكله

        private PlanFeatures() { } // For EF Core

        public PlanFeatures(bool canUseCustomDomain, bool hasAiRecommendations, bool hasAdvancedReports, bool hasPrioritySupports)
        {
            CanUseCustomDomain = canUseCustomDomain;
            HasAiRecommendations = hasAiRecommendations;
            HasAdvancedReports = hasAdvancedReports;
            HasPrioritySupports = hasPrioritySupports;
        }
    }
}
