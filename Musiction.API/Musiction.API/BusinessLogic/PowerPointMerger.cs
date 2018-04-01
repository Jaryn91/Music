﻿
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Musiction.API.BusinessLogic
{
    public class PowerPointMerger
    {
        private uint uniqueId;

        private IFileAndFolderPath _fileAndFolderPath;

        public PowerPointMerger(IFileAndFolderPath fileAndFolderPath)
        {
            _fileAndFolderPath = fileAndFolderPath;
        }

        public string Merge(List<string> files)
        {
            if (files.Count < 1)
                return "";

            string outcomeFilePath = _fileAndFolderPath.GetMergedFilePath();

            try
            {
                File.Copy(files.First(), outcomeFilePath, true);
                files.RemoveAt(0);

                foreach (string sourcePresentation in files)
                    MergeSlides(sourcePresentation, outcomeFilePath);

                return outcomeFilePath;
            }
            catch (Exception ex)
            {
                return $"Error durning merging. {ex.Message}";
            }
        }


        public int GetNumberOfSlides(string filePath)
        {
            using (PresentationDocument mySourceDeck = PresentationDocument.Open(filePath, false))
            {
                PresentationPart sourcePresPart = mySourceDeck.PresentationPart;
                return sourcePresPart.Presentation.SlideIdList.Count();
            }
        }

        private void MergeSlides(string sourcePresentation, string outcomeFilePath)
        {
            int id = 0;

            // Open the destination presentation.
            using (PresentationDocument myDestDeck = PresentationDocument.Open(outcomeFilePath, true))
            {
                PresentationPart destPresPart = myDestDeck.PresentationPart;

                // If the merged presentation does not have a SlideIdList 
                // element yet, add it.
                if (destPresPart.Presentation.SlideIdList == null)
                    destPresPart.Presentation.SlideIdList = new SlideIdList();

                // Open the source presentation. This will throw an exception if
                // the source presentation does not exist.
                using (PresentationDocument mySourceDeck = PresentationDocument.Open(sourcePresentation, false))
                {
                    PresentationPart sourcePresPart = mySourceDeck.PresentationPart;

                    // Get unique ids for the slide master and slide lists
                    // for use later.
                    uint uniqueId = GetMaxSlideMasterId(destPresPart.Presentation.SlideMasterIdList);

                    uint maxSlideId = GetMaxSlideId(destPresPart.Presentation.SlideIdList);

                    // Copy each slide in the source presentation, in order, to 
                    // the destination presentation.
                    foreach (SlideId slideId in
                      sourcePresPart.Presentation.SlideIdList)
                    {
                        SlidePart sp;
                        SlidePart destSp;
                        SlideMasterPart destMasterPart;
                        string relId;
                        SlideMasterId newSlideMasterId;
                        SlideId newSlideId;

                        // Create a unique relationship id.
                        id++;
                        sp = (SlidePart)sourcePresPart.GetPartById(slideId.RelationshipId);

                        relId = sourcePresentation.Remove(sourcePresentation.LastIndexOf('.')) + id;
                        relId = relId.Substring(relId.LastIndexOf('\\') + 1);

                        // Add the slide part to the destination presentation.
                        destSp = destPresPart.AddPart<SlidePart>(sp, relId);

                        // The slide master part was added. Make sure the
                        // relationship between the main presentation part and
                        // the slide master part is in place.
                        destMasterPart = destSp.SlideLayoutPart.SlideMasterPart;
                        destPresPart.AddPart(destMasterPart);

                        // Add the slide master id to the slide master id list.
                        uniqueId++;
                        newSlideMasterId = new SlideMasterId();
                        newSlideMasterId.RelationshipId = destPresPart.GetIdOfPart(destMasterPart);
                        newSlideMasterId.Id = uniqueId;

                        destPresPart.Presentation.SlideMasterIdList.Append(newSlideMasterId);

                        // Add the slide id to the slide id list.
                        maxSlideId++;
                        newSlideId = new SlideId();
                        newSlideId.RelationshipId = relId;
                        newSlideId.Id = maxSlideId;

                        destPresPart.Presentation.SlideIdList.Append(newSlideId);
                    }

                    // Make sure that all slide layout ids are unique.
                    FixSlideLayoutIds(destPresPart, uniqueId);
                }

                // Save the changes to the destination deck.
                destPresPart.Presentation.Save();
            }
        }

        private void FixSlideLayoutIds(PresentationPart presPart, uint uniqueId)
        {
            // Make sure that all slide layouts have unique ids.
            foreach (SlideMasterPart slideMasterPart in
              presPart.SlideMasterParts)
            {
                foreach (SlideLayoutId slideLayoutId in
                  slideMasterPart.SlideMaster.SlideLayoutIdList)
                {
                    uniqueId++;
                    slideLayoutId.Id = (uint)uniqueId;
                }

                slideMasterPart.SlideMaster.Save();
            }
        }

        private uint GetMaxSlideId(SlideIdList slideIdList)
        {
            // Slide identifiers have a minimum value of greater than or
            // equal to 256 and a maximum value of less than 2147483648. 
            uint max = 256;

            if (slideIdList != null)
                // Get the maximum id value from the current set of children.
                foreach (SlideId child in slideIdList.Elements<SlideId>())
                {
                    uint id = child.Id;

                    if (id > max)
                        max = id;
                }

            return max;
        }

        private uint GetMaxSlideMasterId(SlideMasterIdList slideMasterIdList)
        {
            // Slide master identifiers have a minimum value of greater than
            // or equal to 2147483648. 
            uint max = 2147483648;

            if (slideMasterIdList != null)
                // Get the maximum id value from the current set of children.
                foreach (SlideMasterId child in
                  slideMasterIdList.Elements<SlideMasterId>())
                {
                    uint id = child.Id;

                    if (id > max)
                        max = id;
                }

            return max;
        }
    }
}
