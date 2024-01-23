import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityList from './ActivityList';
import ActivityFilters from "./ActivityFilters";

export default observer(function ActivityDashboard() {
    const { activityStore } = useStore();
    const { loadActivities, activityRegistry } = activityStore;

    useEffect(() => {
        if (activityRegistry.size <= 1) loadActivities();
    }, [loadActivities])

    if (activityStore.loadingInitial) return <LoadingComponent content='Loading...' />

    return (
        <Grid>
            {/* Semantic UI Grid is 16 columns */}

            <Grid.Column width='10'>
                <ActivityList />
            </Grid.Column>

            <Grid.Column width='6'>
                <ActivityFilters />
            </Grid.Column>
        </Grid>
    )
})
