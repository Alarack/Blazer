﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour {

    public static StatusManager statusManager;

    public List<StatusEntry> statusEntries = new List<StatusEntry>();

    private void Awake() {
        if (statusManager == null)
            statusManager = this;
        else
            Destroy(this);
    }

    public void Initialize() {

    }

    private void Update() {
        for(int i = 0; i < statusEntries.Count; i++) {
            statusEntries[i].ManagedUpdate();
        }
    }

    public static void AddStatus(Entity target, Status status) {
        int count = statusManager.statusEntries.Count;
        StatusEntry targetEntry = null;

        for(int i = 0; i < count; i++) {
            if(statusManager.statusEntries[i].target == target) {
                targetEntry = statusManager.statusEntries[i];
                break;
            }
        }

        if(targetEntry != null) {
            targetEntry.AddStatus(status);
            return;
        }


        StatusEntry newStatus = new StatusEntry(target, new StatusContainer(status));
        statusManager.statusEntries.Add(newStatus);
    }

    public static void RemoveStatus(Entity target, Status targetStatus) {
        int count = statusManager.statusEntries.Count;
        StatusEntry targetEntry = null;

        for (int i =0; i < count; i++) {
            if(statusManager.statusEntries[i].target == target) {
                targetEntry = statusManager.statusEntries[i];
                //statusManager.statusEntries.Remove(statusManager.statusEntries[i]);
                break;
            }
        }

        if(targetEntry != null) {
            targetEntry.RemoveStatus(targetStatus);
            if(targetEntry.GetStatusCount() < 1) {
                statusManager.statusEntries.Remove(targetEntry);
            }
        }
    }


    [System.Serializable]
    public class StatusEntry {
        public Entity target;
        private StatusContainer statusContainer;

        public StatusEntry(Entity target, StatusContainer statusContainer) {
            this.target = target;
            this.statusContainer = statusContainer;
        }

        public void ManagedUpdate() {
            statusContainer.ManagedUpdate();
        }

        public int GetStatusCount() {
            return statusContainer.activeStatusList.Count;
        }

        public void AddStatus(Status status) {
            statusContainer.AddStatus(status);
        }

        public void RemoveStatus(Status status) {
            statusContainer.RemoveStatus(status);
        }

    }


    [System.Serializable]
    public class StatusContainer {
        public List<Status> activeStatusList = new List<Status>();

        public StatusContainer(Status initialStatus) {
            AddStatus(initialStatus);
        }

        public void AddStatus(Status status) {
            activeStatusList.Add(status);
        }

        public void RemoveStatus(Status status) {
            if (activeStatusList.Contains(status)) {
                activeStatusList.Remove(status);
            }
        }

        public void ManagedUpdate() {
            for (int i = 0; i < activeStatusList.Count; i++) {
                activeStatusList[i].ManagedUpdate();
            }
        }


    }

}
