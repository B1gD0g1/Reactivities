import { Box } from "@mui/material";
import ActivitityCard from "./ActivityCard";

type Props = {
    activities: Activity[];
    selectActivity: (id: string) => void;
}

export default function ActivityList({activities, selectActivity }: Props) {
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
      {activities.map((activity) => (
        <ActivitityCard
          key={activity.id}
          activity={activity}
          selectActivity={selectActivity}
        />
      ))}
    </Box>
  );
}