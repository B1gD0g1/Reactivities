import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import type { FormEvent } from "react";
import { useActivities } from "../../../lib/hooks/useActivities";

type Props = {
    activity?: Activity;
    closeForm: () => void;
}

export default function ActivityFrom({ activity, closeForm }: Props) {
    const { updateActivity, createActivity } = useActivities();

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const formData = new FormData(event.currentTarget);

        const data: {[key: string]: FormDataEntryValue } = {}
        formData.forEach((value, key) => {
            data[key] = value;
        });
        
        if (activity) {
            data.id = activity.id;
            await updateActivity.mutateAsync(data as unknown as Activity);
            closeForm();
        } else {
            await createActivity.mutateAsync(data as unknown as Activity);
            closeForm();
        }

    }

    return (
      <Paper sx={{ borderRadius: 3, padding: 3 }}>
        <Typography variant="h5" gutterBottom color="primary">
          创建活动
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit}
          display="flex"
          flexDirection="column"
          gap={3}
        >
          <TextField name="title" label="标题" defaultValue={activity?.title} />
          <TextField
            name="description"
            label="描述"
            multiline
            rows={3}
            defaultValue={activity?.description}
          />
          <TextField
            name="category"
            label="类别"
            defaultValue={activity?.category}
          />
          <TextField
            name="date"
            label="日期"
            type="date"
            defaultValue={activity?.date 
                ? new Date(activity.date).toISOString().split('T')[0]
                : new Date().toISOString().split('T')[0]}
          />
          <TextField name="city" label="城市" defaultValue={activity?.city} />
          <TextField
            name="venue"
            label="活动地点"
            defaultValue={activity?.venue}
          />
          <Box display="flex" justifyContent="end" gap={3}>
            <Button onClick={closeForm} color="inherit">
              取消
            </Button>
            <Button
              type="submit"
              color="success"
              variant="contained"
              disabled={updateActivity.isPending || createActivity.isPending}
            >
              提交
            </Button>
          </Box>
        </Box>
      </Paper>
    );
}
