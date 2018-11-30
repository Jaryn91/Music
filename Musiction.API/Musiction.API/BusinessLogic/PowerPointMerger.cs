
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using Musiction.API.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Musiction.API.BusinessLogic
{
    public class PowerPointMerger : IMerge
    {
        private readonly IFileAndFolderPathsCreator _fileAndFolderPath;

        public PowerPointMerger(IFileAndFolderPathsCreator fileAndFolderPath)
        {
            _fileAndFolderPath = fileAndFolderPath;
        }

        public string Merge(IEnumerable<Song> songs)
        {
            var paths = new List<string>();
            foreach (var song in songs)
            {
                paths.Add(string.Format(MagicString.UrlToPptxExport, song.PresentationId));
            }

            var finalFileName = Merge(paths);
            return finalFileName;
        }

        private string Merge(List<string> files)
        {
            if (files.Count < 1)
                return "";

            var finalFileName = MagicString.FinalFileName;
            var finalPresentationPath = _fileAndFolderPath.GetPathToMergedFiles(finalFileName);
            using (var client = new WebClient())
            {
                client.DownloadFile(files.First(), finalPresentationPath);
            }

            files.RemoveAt(0);

            foreach (string presentationId in files)
                MergeSlides(presentationId, finalPresentationPath);

            return finalFileName;
        }

        private PresentationDocument GetPresentationDocument(string url, bool isEditable)
        {    //Create a stream for the file
            using (var client = new WebClient())
            {
                var content = client.DownloadData(url);
                var stream = new MemoryStream(content);
                return PresentationDocument.Open(stream, isEditable);
            }
        }

        public int GetNumberOfSlides(string filePath)
        {
            using (var mySourceDeck = PresentationDocument.Open(filePath, false))
            {
                var sourcePresPart = mySourceDeck.PresentationPart;
                return sourcePresPart.Presentation.SlideIdList.Count();
            }
        }

        private void MergeSlides(string presentationId, string finalPresentation)
        {
            var id = 0;

            using (var myDestDeck = PresentationDocument.Open(finalPresentation, true))
            {
                var destPresPart = myDestDeck.PresentationPart;
                // If the merged presentation does not have a SlideIdList 
                // element yet, add it.
                if (destPresPart.Presentation.SlideIdList == null)
                    destPresPart.Presentation.SlideIdList = new SlideIdList();

                // Open the source presentation. This will throw an exception if
                // the source presentation does not exist.
                using (var mySourceDeck = GetPresentationDocument(presentationId, false))
                {
                    var sourcePresPart = mySourceDeck.PresentationPart;

                    // Get unique ids for the slide master and slide lists
                    // for use later.
                    var uniqueId = GetMaxSlideMasterId(destPresPart.Presentation.SlideMasterIdList);

                    var maxSlideId = GetMaxSlideId(destPresPart.Presentation.SlideIdList);

                    // Copy each slide in the source presentation, in order, to 
                    // the destination presentation.
                    foreach (var openXmlElement in
                      sourcePresPart.Presentation.SlideIdList)
                    {
                        var slideId = (SlideId)openXmlElement;
                        // Create a unique relationship id.
                        id++;
                        var sp = (SlidePart)sourcePresPart.GetPartById(slideId.RelationshipId);

                        var result = GetUniqueIdOfPresentation(presentationId);

                        var relId = result + id;

                        // Add the slide part to the destination presentation.
                        var destSp = destPresPart.AddPart<SlidePart>(sp, relId);

                        // The slide master part was added. Make sure the
                        // relationship between the main presentation part and
                        // the slide master part is in place.
                        var destMasterPart = destSp.SlideLayoutPart.SlideMasterPart;
                        destPresPart.AddPart(destMasterPart);

                        // Add the slide master id to the slide master id list.
                        uniqueId++;
                        var newSlideMasterId = new SlideMasterId
                        {
                            RelationshipId = destPresPart.GetIdOfPart(destMasterPart),
                            Id = uniqueId
                        };

                        destPresPart.Presentation.SlideMasterIdList.Append(newSlideMasterId);

                        // Add the slide id to the slide id list.
                        maxSlideId++;
                        var newSlideId = new SlideId
                        {
                            RelationshipId = relId,
                            Id = maxSlideId
                        };

                        destPresPart.Presentation.SlideIdList.Append(newSlideId);
                    }

                    // Make sure that all slide layout ids are unique.
                    FixSlideLayoutIds(destPresPart, uniqueId);
                }

                //// Save the changes to the destination deck.
                destPresPart.Presentation.Save();
            }
        }

        private string GetUniqueIdOfPresentation(string presentationId)
        {
            int pFrom = presentationId.IndexOf(@"presentation/d/", StringComparison.Ordinal) + @"presentation/d/".Length;
            int pTo = presentationId.LastIndexOf(@"/export/pptx", StringComparison.Ordinal);

            var result = presentationId.Substring(pFrom, pTo - pFrom);
            Regex rgx = new Regex("[^a-zA-Z]");
            result = rgx.Replace(result, "");
            return result;
        }

        private void FixSlideLayoutIds(PresentationPart presPart, uint uniqueId)
        {
            // Make sure that all slide layouts have unique ids.
            foreach (var slideMasterPart in
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

            if (slideIdList == null)
                return max;

            foreach (var child in slideIdList.Elements<SlideId>())
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
                foreach (var child in
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
