# Exercise for candidates for BA positions for ParcelVision

## Objective

Discuss and explore the topic in order to come to a shared understanding of what is necessary to find a solution to the problem. The primary objective is to exercise some of the soft skills interacting with the development team, to demonstrate some of the viable analysis approaches to widen the knowledge of the area and to practice working as an equal member of the team.

Fully and successfully completing the exercise (coming up with a solution we all agree on) will not be counted as a benefit (although it won't hurt) in an attempt to not put people familiar with the domain in an unfair advantage, even if we made every attempt to make sure that all the necessary information was provided below.

## Background

The carriers publish their price lists based on many factors including the collection and destination addresses (starting point and endpoint of the shipping journey).

We need to support quotes calculations for all carriers. For the purpose of this exercise, the price only changes depending collection and destination address, and all other factors will be ignored.

Carriers tend to group these addresses into higher-order categories, such as postcode area, city, province, country, or group of countries (e.g. Western Europe), often called "zones". Prices are calculated from a given zone to the same or another zone.

The various carriers have different views on what belongs to each of these zones. For instance, some treat the UK as a whole, some calculate prices differently for GB vs Northern Ireland, and some might even consider the whole of the island of Ireland as a single unit from the pricing perspective. Other examples include the Canary Islands, which are a part of Spain but are treated as a different entity for pricing purposes by some of the carriers. Others publish a list of postcodes for mainland Spain and a different list of postcodes for more remote parts of the country (including the Canary Islands).

## Feature request (what needs analysis)

Given a quote request, we want to return a list of prices we have discovered from all carriers.

### Assumptions

You can assume that:

- We know precisely where we are shipping from and to
- Each carrier has exactly one price only for a particular combination of collection and delivery
- We ignore all but one service from each carrier
- No surcharges apply to any collection and delivery combination

### Important!

- Reaching a solution by the end is NOT considered positive at all. You do not need to make sure you have a solution coming in, nor worry that you need to come to a solution by the end. We spent a good amount of weeks on this problem, and we don't expect to have a solution in 30 minutes. 
- Prior research into the subject matter is **not required**. The information in this document is absolutely everything you need to know about this exercise.
- You should not come into the interview with a solution prepared, although feel free to prepare something to demonstrate your understanding of what has been said here. This can be as simple as some questions you ask us during the interview. To emphasize that you don't need to come in the interview with a solution prepared consider that what you come up alone without domain experts at your disposal, and only a fraction of the time we invested, is almost certainly not going to be something which meets all of our particular requirements.
- Please do not spend a lot of time researching anything outside this document. The **only** purposes of this exercise is to assess your ability to extract requirements from our domain expert, your analytical, and communication skills, and researching information outside this document (which may be too big or not accurate) may affect this negatively.

## General info

This exercise will be held during a live session with multiple people. The information provided above is to help you, the candidate, be prepared. We will all be trying to _actively help you do your absolute best_ during the interview. If you have any questions prior to the interview, please feel free to create issues in this repo (do not feel obliged to do so; anything you do prior to the final stage interview isn't going to be taken into consideration when making a decision about who to employ).

Time allocated: 20 minutes

Participants:

- You
- Existing Business Analyst
- Developer
- Subject Matter Expert

## Glossary of terms

| Term        | Definition           | Examples  |
| ------------- |-------------| -----|
| Carrier     | A company that offers delivery by road, air or sea | DPD, Hermes, DHL |
| Quote     | List of carriers which can collect from a provided collection point, and deliver to a delivery location, including the costs for each  | DPD Express: £5.99, Hermes Next Day: £4.99|
| Shipping     | The physical process of transporting goods | Truck, train  |
