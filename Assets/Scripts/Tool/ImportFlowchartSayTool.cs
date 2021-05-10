using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using Ideafixxxer.CsvParser;

namespace Fungus
{

    public class ImportFlowchartSayTool : MonoBehaviour
    {
        [SerializeField] protected TextAsset localizationFile;
        [SerializeField] public List<Character> characters = new List<Character>();
        public Flowchart targetFlowchart;
        public string BlockName;
        public bool changeGameObjectNameToTextFileName = false;
        public bool saveAsPrefab = false;
        public string savePrefabPath = "Assets/Resources/Prefab/Flowchart_SayDialog/";
        [ContextMenu("ImportCSVData")]
        public void ImportCSVData()
        {
            CsvParser csvParser = new CsvParser();
            string[][] csvTable = csvParser.ParseTool(localizationFile.text);
            if (csvTable.Length <= 0)
            {
                // No data rows in file
                return;
            }

            if (targetFlowchart == null)
            {
                // No target Flowchart
                return;
            }

            if(changeGameObjectNameToTextFileName)
            {
                gameObject.name = localizationFile.name;
            }

            if (BlockName.Length < 1)
                BlockName = targetFlowchart.SelectedBlock.BlockName;
            Block targetBlock = targetFlowchart.FindBlock(BlockName);
            // Parse header row
            for (int i = 0; i < csvTable.Length; ++i)
            {
                string[] fields = csvTable[i];

                //import character name if is non is Narrator
                string characterName = fields[0];
                string dialogue = fields[1];
                AddCommandCallback(targetBlock, characterName, dialogue);
            }
            //sort ItemId
            List<Command> sort = targetBlock.CommandList;
            sort.Sort((x, y) => { return x.ItemId.CompareTo(y.ItemId); });
            if(saveAsPrefab)
            PrefabUtility.SaveAsPrefabAsset(this.gameObject, savePrefabPath+gameObject.name+".prefab");
            Debug.Log("ImportComplect");
        }

        void AddCommandCallback(Block curBlock,string name,string dialogue)
        {
            var block = curBlock;
            if (block == null)
            {
                return;
            }

            var flowchart = (Flowchart)block.GetFlowchart();

            // Use index of last selected command in list, or end of list if nothing selected.
            int index = -1;
            foreach (var command in flowchart.SelectedCommands)
            {
                if (command.CommandIndex + 1 > index)
                {
                    index = command.CommandIndex + 1;
                }
            }
            if (index == -1)
            {
                index = block.CommandList.Count;
            }

            var newCommand = Undo.AddComponent<Say>(block.gameObject);
            newCommand.SetStandardText(dialogue);
            //var activeCharacters = Character.ActiveCharacters;
            newCommand._Character = characters.Find(a => a.NameText == name);

            block.GetFlowchart().AddSelectedCommand(newCommand);
            newCommand.ParentBlock = block;
            newCommand.ItemId = flowchart.NextItemId();

            // Let command know it has just been added to the block
            newCommand.OnCommandAdded(block);

            Undo.RecordObject(block, "Set command type");
            if (index < block.CommandList.Count - 1)
            {
                block.CommandList.Insert(index, newCommand);
            }
            else
            {
                block.CommandList.Add(newCommand);
            }

            // Because this is an async call, we need to force prefab instances to record changes
            PrefabUtility.RecordPrefabInstancePropertyModifications(block);

            flowchart.ClearSelectedCommands();

        }
    }
}
