# Know Priorities

[@KnowPriorities](https://twitter.com/knowpriorities) | [Website](http://KnowPriorities.com/) | [Podcast](http://talkabout.knowpriorities.com/) | [Email Course](https://app.convertkit.com/landing_pages/2368)

Prioritization engine designed to calculate a final priority list of actual value.

MIT Licensed for everyone's benefit.

## Getting Started

* [NuGet "install-package KnowPriorities"](https://www.nuget.org/packages/KnowPriorities/)
* [~Docs](http://prioritize.io/Docs)
* [Experiment in Excel](https://gumroad.com/l/priority-calculator-spreadsheet)
* [Integrate via http](http://prioritize.io/)

## Features

### v1

* [x] Injest priority lists
* [x] Injest ranges (numeric high/low; ex. on a scale of 1-5)
* [x] Injest series (ex. a, b, c, d, e, f)
* [x] Injest behaviors (ex. systems of likes, comments, upvotes, views, heat, etc)
* [x] Provide feedback bridge (supporting ranges, series, and behaviors)
* [x] Control stakeholder volume including mute (aka assessment of how their input is valued)
* [x] Control group level % of say overall (ex. customers get 40% of the final say)
* [x] Stakeholder equality (stakeholder value does not change due to "squeaky wheel" or lack there of)
* [x] Population balancing (even with 1M customers, 5 employees are heard equally before % of say)
* [x] Global progressive system of values (uses golden ratio represented as fibonacci values)
* [x] Include work items' scope
* [ ] Include work items' opportunity
* [x] Tagging to adjust or set stakeholder volume
* [x] Tagging to adjust or set item scope
* [x] Group level tag overrides for adjusting stakeholder volume
* [x] Group level tag overrides for adjusting or setting item scope
* [x] Depreciation of stakeholder input (ex. after 30 days person A's input is valued 33% less)
* [x] Group level results
* [x] Overall results
* [x] Results include distance of value
* [x] Support for medium-large amount of stakeholders (tested >1M; crashes at 10M)
* [x] Support Json injestion of data model
 
### v2

Goals are still being reviewed for v2.  However, three things are primary:  

* Raising the cap to support a theoretical unlimited number of stakeholders
* Supporting rolling update calculations instead of full load calculations
* Refocusing on the core with only priority lists as the injestion point (not ranges, series, or behaviors)

v2 is being built along side v1 as sub-namespacing so v2 can rollout in stages with v1 intact while also allowing v1 to support bug fixes.  This also allows a relatively stable v1 to v2 comparison to occur to help ensure the results are stable or improving from version to version.
