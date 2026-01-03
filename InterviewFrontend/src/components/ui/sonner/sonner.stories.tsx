import type { Meta, StoryObj } from '@storybook/react'
import { Toaster } from './sonner'
import { toast } from 'sonner'
import { Button } from '../button/button'
import { useEffect } from 'react'
import { createPortal } from 'react-dom'

// Global flag to ensure only one Toaster is rendered in Docs view
let toasterRendered = false

const meta = {
  title: 'UI/Toaster',
  component: Toaster,
  parameters: {
    layout: 'centered',
  },
  tags: ['autodocs'],
  decorators: [
    (Story) => {
      const shouldRenderToaster = !toasterRendered

      useEffect(() => {
        toasterRendered = true
        return () => {
          // Reset when all stories unmount
          const toasters = document.querySelectorAll('[data-sonner-toaster]')
          if (toasters.length === 0) {
            toasterRendered = false
          }
        }
      }, [])

      return (
        <>
          {shouldRenderToaster && createPortal(<Toaster />, document.body)}
          <Story />
        </>
      )
    },
  ],
} satisfies Meta<typeof Toaster>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  render: () => (
    <Button onClick={() => toast('This is a default toast message')}>
      Show Toast
    </Button>
  ),
}

export const Success: Story = {
  render: () => (
    <Button onClick={() => toast.success('Operation completed successfully!')}>
      Show Success
    </Button>
  ),
}

export const Error: Story = {
  render: () => (
    <Button
      variant="destructive"
      onClick={() => toast.error('Something went wrong!')}
    >
      Show Error
    </Button>
  ),
}

export const Warning: Story = {
  render: () => (
    <Button onClick={() => toast.warning('Please be careful with this action')}>
      Show Warning
    </Button>
  ),
}

export const Info: Story = {
  render: () => (
    <Button onClick={() => toast.info('Here is some information for you')}>
      Show Info
    </Button>
  ),
}

export const Loading: Story = {
  render: () => (
    <Button onClick={() => toast.loading('Loading, please wait...')}>
      Show Loading
    </Button>
  ),
}

export const WithDescription: Story = {
  render: () => (
    <Button
      onClick={() =>
        toast('New message received', {
          description: 'You have a new message from John Doe',
        })
      }
    >
      Show with Description
    </Button>
  ),
}

export const WithAction: Story = {
  render: () => (
    <Button
      onClick={() =>
        toast('Event scheduled', {
          description: 'Your meeting is scheduled for tomorrow at 2 PM',
          action: {
            label: 'View',
            onClick: () => console.log('View clicked'),
          },
        })
      }
    >
      Show with Action
    </Button>
  ),
}

export const PromiseToast: Story = {
  render: () => (
    <Button
      onClick={() => {
        const promise = () =>
          new Promise((resolve) => setTimeout(() => resolve({ name: 'Data' }), 2000))

        toast.promise(promise, {
          loading: 'Loading data...',
          success: 'Data loaded successfully!',
          error: 'Failed to load data',
        })
      }}
    >
      Show Promise Toast
    </Button>
  ),
}

export const CustomDuration: Story = {
  render: () => (
    <Button
      onClick={() =>
        toast('This toast will disappear in 10 seconds', {
          duration: 10000,
        })
      }
    >
      Show with Custom Duration
    </Button>
  ),
}

export const Dismissible: Story = {
  render: () => (
    <Button
      onClick={() =>
        toast('You can dismiss this by clicking the X', {
          dismissible: true,
        })
      }
    >
      Show Dismissible
    </Button>
  ),
}

export const AllTypes: Story = {
  render: () => (
    <div className="flex flex-col gap-2">
      <Button onClick={() => toast('Default notification')}>Default</Button>
      <Button onClick={() => toast.success('Success notification')}>
        Success
      </Button>
      <Button onClick={() => toast.error('Error notification')}>Error</Button>
      <Button onClick={() => toast.warning('Warning notification')}>
        Warning
      </Button>
      <Button onClick={() => toast.info('Info notification')}>Info</Button>
      <Button onClick={() => toast.loading('Loading notification')}>
        Loading
      </Button>
    </div>
  ),
}

export const Multiple: Story = {
  render: () => (
    <Button
      onClick={() => {
        toast('First notification')
        setTimeout(() => toast.success('Second notification'), 500)
        setTimeout(() => toast.error('Third notification'), 1000)
      }}
    >
      Show Multiple Toasts
    </Button>
  ),
}

export const Positioned: Story = {
  parameters: {
    docs: {
      description: {
        story: 'Note: Position is controlled by the global Toaster component in preview.tsx. This story demonstrates the toast function only.',
      },
    },
  },
  render: () => (
    <div className="flex flex-col gap-4">
      <p className="text-sm text-muted-foreground mb-2">
        Position can be configured on the Toaster component (currently set globally)
      </p>
      <Button onClick={() => toast('Toast notification')}>Show Toast</Button>
    </div>
  ),
}
